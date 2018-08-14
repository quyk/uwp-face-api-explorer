using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Models
{
    public class Group
    {
        [JsonProperty(PropertyName = "personGroupId")]
        public string PersonGroupId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "userData")]
        public string UserData { get; set; }
    }
}
