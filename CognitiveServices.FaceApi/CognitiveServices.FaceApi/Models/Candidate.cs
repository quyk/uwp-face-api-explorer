using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Models
{
    public class Candidate
    {
        [JsonProperty(PropertyName = "personId")]
        public string PersonId { get; set; }
        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }
    }
}
