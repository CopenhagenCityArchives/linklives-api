using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;


namespace linklives_api_dal.Repositories
{
    public class ESPersonAppearanceRepository : IPersonAppearanceRepository
    {
        private readonly ElasticClient client;

        public ESPersonAppearanceRepository(ElasticClient client)
        {
            this.client = client;
        }

        public JObject GetById(string Id)
        {
            var PasSearchResponse = client.LowLevel.Get<StringResponse>("pas", Id);
            var pas = (JObject)JObject.Parse(PasSearchResponse.Body)["_source"]["person_appearance"];

            var SourceSearchResponse = client.LowLevel.Get<StringResponse>("sources", (string)pas["source_id"]);
            pas.Add("source", JObject.Parse(SourceSearchResponse.Body)["_source"]["source"]);

            return pas;
        }

        public List<dynamic> GetByIds(List<string> Ids)
        {
            var pasSearchResponse = client.Search<dynamic>(s => s
            .Index("pas")
            .From(0)
            .Size(100)
            .Query(q => q
                    .Nested(n => n
                    .Path("person_appearance")
                    .Query(nq => nq
                        .Terms(t => t
                            .Field("person_appearance.id")
                            .Terms(Ids))))));
            var pass = pasSearchResponse.Documents.Select(x => x["person_appearance"]).ToList();
            var sourceids = pass.Select(p => (int)p["source_id"]);
            var sourceSearchResponse = client.Search<dynamic>(s => s
            .Index("sources")
            .From(0)
            .Size(100)
            .Query(q => q
                    .Nested(n => n
                    .Path("source")
                    .Query(nq => nq
                        .Terms(t => t
                            .Field("source.source_id")
                            .Terms(sourceids))))));
            foreach (var pas in pass)
            {
                pas.Add("source", sourceSearchResponse.Documents.Single(s => (int)s["source"]["source_id"] == (int)pas["source_id"])["source"]);
            }
            return pass;
        }
    }
}
