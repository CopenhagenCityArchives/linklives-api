using Newtonsoft.Json;

namespace linklives_api_dal.domain
{
    public class PersonAppearance
    {
        [JsonProperty(PropertyName = "name_display")]
        public string Name_display { get; set; }
        [JsonProperty(PropertyName = "birthyear_display")]
        public string Birthyear_display { get; set; }
        [JsonProperty(PropertyName = "role_display")]
        public string Role_display { get; set; }
        [JsonProperty(PropertyName = "birthplace_display")]
        public string Birthplace_display { get; set; }
        [JsonProperty(PropertyName = "occupation_display")]
        public string Occupation_display { get; set; }
        [JsonProperty(PropertyName = "sourceplace_display")]
        public string Sourceplace_display { get; set; }
    }
}