using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Models
{
    public class Face
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
