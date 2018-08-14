using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Models
{
    public class Error
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
