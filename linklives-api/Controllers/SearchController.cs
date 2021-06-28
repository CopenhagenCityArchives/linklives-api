﻿using Elasticsearch.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get(string indexes,[FromBody]JsonElement query)
        {
            var searchResponse = client.LowLevel.Search<StringResponse>(indexes, query.ToString());

            return StatusCode(searchResponse.HttpStatusCode.HasValue ? searchResponse.HttpStatusCode.Value : 0, searchResponse.Body);
        }
    }
}
