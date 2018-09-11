using System.Collections.Generic;
using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Models
{
    public class Identify
    {
        [JsonProperty(PropertyName = "confidenceThreshold")]
        public double ConfidenceThreshold { get; set; }
        [JsonProperty(PropertyName = "faceIds")]
        public IList<string> FaceIds { get; set; }
        [JsonProperty(PropertyName = "personGroupId")]
        public string PersonGroupId { get; set; }
    }
}
