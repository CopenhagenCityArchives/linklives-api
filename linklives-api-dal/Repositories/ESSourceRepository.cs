using linklives_api_dal.domain;
using Nest;
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
        private readonly ElasticClient client;

        public ESSourceRepository(ElasticClient client)
        {
            this.client = client;
        }

        public List<Source> GetAll()
        {
            var searchResponse = client.Search<Source>(s => s
            .Index("sources")
            .Query(q => q.MatchAll()));
            return searchResponse.Documents.ToList();
        }
    }
}
