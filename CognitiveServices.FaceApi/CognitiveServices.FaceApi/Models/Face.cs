using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Models
{
    public class Face
    {
        [JsonProperty(PropertyName = "persistedFaceId")]
        public string PersistedFaceId { get; set; }
        [JsonIgnore]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "userData")]
        public string UserData { get; set; }
    }
}
