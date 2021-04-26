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

        public PersonAppearance GetById(int PaId)
        {
            return JsonSerializer.Deserialize<PersonAppearance>(GetRawJsonById(PaId));
        }

        public string GetRawJsonById(int PaId)
        {
            throw new NotImplementedException();
            //TODO: Write proper query
            var searchResponse = client.Search<StringResponse>("people", PostData.Serializable(new
            {
                from = 0,
                size = 10,
                query = new
                {
                    match = new
                    {
                        firstName = new
                        {
                            query = "Martijn"
                        }
                    }
                }
            }));

            return searchResponse.Body;
        }
    }
}
