using CognitiveServices.FaceApi.Models.Services.Attributes;

namespace CognitiveServices.FaceApi.Models.Services
{
    public class FaceLandmarks
    {
        public PupilLeft PupilLeft { get; set; }
        public PupilRight PupilRight { get; set; }
        public NoseTip NoseTip { get; set; }
        public MouthLeft MouthLeft { get; set; }
        public MouthRight MouthRight { get; set; }
        public EyebrowLeftOuter EyebrowLeftOuter { get; set; }
        public EyebrowLeftInner EyebrowLeftInner { get; set; }
        public EyeLeftOuter EyeLeftOuter { get; set; }
        public EyeLeftTop EyeLeftTop { get; set; }
        public EyeLeftBottom EyeLeftBottom { get; set; }
        public EyeLeftInner EyeLeftInner { get; set; }
        public EyebrowRightInner EyebrowRightInner { get; set; }
        public EyebrowRightOuter EyebrowRightOuter { get; set; }
        public EyeRightInner EyeRightInner { get; set; }
        public EyeRightTop EyeRightTop { get; set; }
        public EyeRightBottom EyeRightBottom { get; set; }
        public EyeRightOuter EyeRightOuter { get; set; }
        public NoseRootLeft NoseRootLeft { get; set; }
        public NoseRootRight NoseRootRight { get; set; }
        public NoseLeftAlarTop NoseLeftAlarTop { get; set; }
        public NoseRightAlarTop NoseRightAlarTop { get; set; }
        public NoseLeftAlarOutTip NoseLeftAlarOutTip { get; set; }
        public NoseRightAlarOutTip NoseRightAlarOutTip { get; set; }
        public UpperLipTop UpperLipTop { get; set; }
        public UpperLipBottom UpperLipBottom { get; set; }
        public UnderLipTop UnderLipTop { get; set; }
        public UnderLipBottom UnderLipBottom { get; set; }
    }
}
