using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Nest;
using System.Text.Json;
using Linklives.Serialization;
using System;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using Linklives.DAL;
using Linklives.Domain;
using System.Security.Claims;

namespace linklives_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ElasticClient client;
        private readonly IEFLifeCourseRepository lifecourseRepository;
        private readonly IKeyedRepository<LifeCourse> esLifecourseRepository;
        private readonly IPersonAppearanceRepository paRepository;
        private readonly IEFDownloadHistoryRepository downloadHistoryRepository;

        public SearchController(
            ElasticClient client,
            IEFLifeCourseRepository lifecourseRepository,
            IKeyedRepository<LifeCourse> esLifecourseRepository,
            IPersonAppearanceRepository paRepository,
            IEFDownloadHistoryRepository downloadHistoryRepository
        )
        {
            this.client = client;
            this.lifecourseRepository = lifecourseRepository;
            this.esLifecourseRepository = esLifecourseRepository;
            this.paRepository = paRepository;
            this.downloadHistoryRepository = downloadHistoryRepository;
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
        [Authorize]
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
            var lifecourses = esLifecourseRepository.GetByKeys(lifecourseKeys);

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
                }).ToArray();

            var linksRows = lifecourses.SelectMany((lifecourse) => {
                return lifecourse.Links.Select((link) => {
                    return new Dictionary<string, (string, Exportable)> {
                        ["life_course_id"] = (lifecourse.Life_course_id.ToString(), new Exportable(FieldCategory.Identification)),
                        ["link_id"] = (link.Link_id, new Exportable(FieldCategory.Identification)),
                        ["pa_id1"] = (link.Pa_id1.ToString(), new Exportable(FieldCategory.Identification, extraWeight: 1)),
                        ["pa_id2"] = (link.Pa_id2.ToString(), new Exportable(FieldCategory.Identification, extraWeight: 2)),
                        ["method_id"] = (link.Method_id, new Exportable(extraWeight: 3)),
                        ["score"] = (link.Score, new Exportable(extraWeight: 4)),
                    };
                });
            }).ToArray();

            var sheet = encoder.Encode(new Dictionary<string, Dictionary<string, (string, Exportable)>[]>{
                ["Search result"] = SpreadsheetSerializer.SerializeAll(orderedQualifiedResults),
                ["Links"] = linksRows,
            });

            downloadHistoryRepository.RegisterDownload(new DownloadHistoryEntry(
                DownloadType.SearchResult,
                $"indices={indexes};" + data.ToString(),
                User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value
            ));
            downloadHistoryRepository.Save();

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
