using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.domain
{
    public class Source
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "source_id")]
        public int Source_id { get; set; }
        [JsonProperty(PropertyName = "year")]
        public int? Year { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }
        [JsonProperty(PropertyName = "institution")]
        public string Institution { get; set; }
    }
    public class SourceIndex
    {
        [JsonProperty(PropertyName = "source")]
        public Source Source { get; set; }
    }
}
