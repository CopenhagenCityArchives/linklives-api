using Newtonsoft.Json;

namespace linklives_api_dal.domain
{
    public class PersonAppearance
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
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
        [JsonProperty(PropertyName = "event_type")]
        public string Event_type { get; set; }
        [JsonProperty(PropertyName = "source_id")]
        public string Source_id { get; set; }
        public Source Source { get; set; }
    }
    public class PAIndex
    {
        [JsonProperty(PropertyName = "person_appearance")]
        public PersonAppearance Person_appearance { get; set; }
    }
}