using linklives_api_dal.domain;
using Nest;
using System.Collections.Generic;
using System.Linq;

namespace linklives_api_dal.Repositories
{
    public class ESSourceRepository : ISourceRepository
    {
        private readonly ElasticClient client;

        public ESSourceRepository(ElasticClient client)
        {
            this.client = client;
        }

        public List<dynamic> GetAll()
        {
            var searchResponse = client.Search<dynamic>(s => s
            .Size(1000)
            .Index("sources")
            .Query(q => q.MatchAll()));
            return searchResponse.Documents.Select(x => x["source"]).ToList();
        }
    }
}
