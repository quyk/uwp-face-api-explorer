using System.Collections.Generic;

namespace CognitiveServices.FaceApi.Models.Services
{
    public class Hair
    {
        public double Bald { get; set; }
        public bool Invisible { get; set; }
        public List<HairColor> HairColor { get; set; }
    }
}
