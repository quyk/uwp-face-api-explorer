using System.Collections.Generic;
using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "personId")]
        public string PersonId { get; set; }
        [JsonProperty(PropertyName = "persistedFaceIds")]
        public IList<string> PersistedFaces { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "userData")]
        public string UserData { get; set; }
    }
}
