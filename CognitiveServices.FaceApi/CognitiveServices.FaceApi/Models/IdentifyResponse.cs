using System.Collections.Generic;
using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Models
{
    public class IdentifyResponse
    {
        [JsonProperty(PropertyName = "faceId")]
        public string FaceId { get; set; }
        [JsonProperty(PropertyName = "candidates")]
        public IList<Candidate> Candidates { get; set; }
    }
}
