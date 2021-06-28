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
    public class ESPersonAppearanceRepository : IPersonAppearanceRepository
    {
        private readonly ElasticClient client;

        public ESPersonAppearanceRepository(ElasticClient client)
        {
            this.client = client;
        }

        public PersonAppearance GetById(string Id)
        {
            var searchResponse = client.Search<PAIndex>(s => s
            .Index("pas")
            .From(0)
            .Size(100)
            .Query(q => q
                    .Nested(n => n
                    .Path("person_appearance")
                    .Query(nq => nq
                        .Terms(t => t
                            .Field(f => f.Person_appearance.Id)
                            .Terms(Id))))));
            return searchResponse.Documents.SingleOrDefault().Person_appearance;
        }
    }
}
