using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nest;
using System.Text.Json;

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
    }
}
