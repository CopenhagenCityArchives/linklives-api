using Elasticsearch.Net;
using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public class ESSourceRepository : ISourceRepository
    {
        private readonly ElasticLowLevelClient client;

        public ESSourceRepository(ElasticLowLevelClient client)
        {
            this.client = client;
        }

        public List<Source> GetAll()
        {
            return JsonSerializer.Deserialize<List<Source>>(GetAllRawJson());
        }

        public string GetAllRawJson()
        {
            var query = @"
            {               
                ""query"": {
                    ""match_all"": { }
                            }
             }";
            var searchResponse = client.Search<StringResponse>("sources", query);

            return searchResponse.Body;
        }
    }
}
