﻿using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nest;
using System.Text.Json;
using Linklives.Serialization;
using System;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using Linklives.DAL;
using Linklives.Domain;

namespace linklives_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ElasticClient client;
        private readonly IEFLifeCourseRepository lifecourseRepository;
        private readonly IPersonAppearanceRepository paRepository;

        public SearchController(
            ElasticClient client,
            IEFLifeCourseRepository lifecourseRepository,
            IPersonAppearanceRepository paRepository
        )
        {
            this.client = client;
            this.lifecourseRepository = lifecourseRepository;
            this.paRepository = paRepository;
        }

        //TODO Delete. This endpoint is not longer in use
        [HttpPost("{indexes}")]
        public IActionResult Get(string indexes, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonElement query)
        {
            var stringquery = query.ToString();
            //If no query was supplied in the request we will assume they just want to get all documents in the specified index
            if (string.IsNullOrEmpty(stringquery))
            {
                stringquery = @"{""size"" : 100, ""query"": { ""match_all"": { } } }";
            }
            var searchResponse = client.LowLevel.Search<StringResponse>(indexes, stringquery);

            return StatusCode(searchResponse.HttpStatusCode.HasValue ? searchResponse.HttpStatusCode.Value : 0, searchResponse.Body);
        }

        [HttpPost("{indexes}/download.{format}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(206)]
        [ProducesResponseType(404)]
        public ActionResult Download(string indexes, string format, [FromBody] Newtonsoft.Json.Linq.JObject data)
        {
            var encoder = Encoder.ForFormat(format);
            if (encoder == null) {
                return NotFound("No formatter for that format exists.");
            }

            var stringData = data.ToString(Newtonsoft.Json.Formatting.None);
            var response = client.LowLevel.Search<StringResponse>(indexes, PostData.String(stringData));

            if(response.HttpStatusCode >= 400 && response.HttpStatusCode < 500) {
                return StatusCode((int)response.HttpStatusCode, $"Bad input for search. ElasticSearch error: {response.Body}");
            }

            if(response.HttpStatusCode != 200) {
                return StatusCode(500, "Something went wrong! Failed to load search result data from ElasticSearch");
            }

            IEnumerable<(string key, string type)> results;
            try {
                var result = JObject.Parse(response.Body);
                var hits = (JArray)result.SelectToken("hits.hits");
                results = hits.Values<JObject>().Select(hit => {
                    var key = (string)hit.SelectToken("_source.key");
                    var index = (string)hit.SelectToken("_index");
                    var type = index.Substring(0, index.IndexOf("_"));
                    return (key, type);
                });
            }
            catch(Exception e) {
                return StatusCode(500, $"Failed to parse search result: {e.Message}\n{e}");
            }

            var paKeys = results
                .Where(result => result.type == "pas")
                .Select(result => result.key)
                .ToList();

            var lifecourseKeys = results
                .Where(result => result.type == "lifecourses")
                .Select(result => result.key)
                .ToList();

            var pas = paRepository.GetByIds(paKeys);
            var lifecourses = lifecourseRepository.GetByKeys(lifecourseKeys);

            foreach(var lifecourse in lifecourses) {
                GetPAsLinksAndLinkRatings(lifecourse);
            }

            // Create a list of all results that respects the order of results from search
            var orderedQualifiedResults = results
                .Select(result => {
                    if(result.type == "pas") {
                        return (object)pas.First(pa => pa.Key == result.key);
                    }
                    return (object)lifecourses.First(lc => lc.Key == result.key);
                });

            var rows = SpreadsheetSerializer.Serialize(orderedQualifiedResults);
            var sheet = encoder.Encode("Search result", rows);

            return File(sheet, encoder.ContentType, $"searchresult.{format}");
        }

        //TODO: currently duplicated from lifecoursecontroller - deduplicate!
        private void GetPAsLinksAndLinkRatings(LifeCourse lifecourse)
        {
            //Fetch person apperances and add them to the lifecourse
            lifecourseRepository.GetLinksAndRatings(lifecourse);
            lifecourse.PersonAppearances = paRepository.GetByIds(lifecourse.Links.SelectMany(l => new[] { $"{l.Source_id1}-{l.Pa_id1}", $"{l.Source_id2}-{l.Pa_id2}" }).Distinct().ToList()).ToList();
        }
    }
}
