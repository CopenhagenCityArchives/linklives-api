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
    public class ESPersonAppearanceRepository : IPersonAppearanceRepository
    {
        private readonly ElasticLowLevelClient client;

        public ESPersonAppearanceRepository(ElasticLowLevelClient client)
        {
            this.client = client;
        }

        public PersonAppearance GetById(string Id)
        {
            return JsonSerializer.Deserialize<PersonAppearance>(GetRawJsonById(Id));
        }

        public string GetRawJsonById(string Id)
        {
            var query = @"
            {
                ""from"": 0,
                ""size"": 100,
                ""query"": {
                            ""bool"": {
                                ""must"": [
                                    {
                                    ""nested"": {
                                        ""path"": ""person_appearance"",
                                    ""query"": {
                                            ""term"": {
                                                ""person_appearance.id"": {
                                                    ""value"": """ + Id + @"""
                                                }
                                            }
                                        }
                                    }
                                }
                        ]
                    }
                }
            }";
            var searchResponse = client.Search<StringResponse>("pas", query);

            return searchResponse.Body;
        }
    }
}
