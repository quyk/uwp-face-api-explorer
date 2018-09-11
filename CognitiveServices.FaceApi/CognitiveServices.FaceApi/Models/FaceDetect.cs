using CognitiveServices.FaceApi.Models.Services;

namespace CognitiveServices.FaceApi.Models
{
    public class FaceDetect
    {
        public string FaceId { get; set; }
        public Rectangle FaceRectangle { get; set; }
        public FaceAttributes FaceAttributes { get; set; }
    }
}