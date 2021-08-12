using Elasticsearch.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace linklives_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ElasticClient client;

        public SearchController(ElasticClient client)
        {
            this.client = client;
        }
        [HttpPost("{indexes}")]
        public IActionResult Get(string indexes, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] JsonElement query)
        {
            var stringquery = query.ToString();
            //If no query was supplied in the request we will assume they just want to get all documents in the specified index
            if (string.IsNullOrEmpty(stringquery))
            {
                stringquery = @"{""size"" : 1000, ""query"": { ""match_all"": { } } }";
            }
            var searchResponse = client.LowLevel.Search<StringResponse>(indexes, stringquery);

            return StatusCode(searchResponse.HttpStatusCode.HasValue ? searchResponse.HttpStatusCode.Value : 0, searchResponse.Body);
        }
    }
}
