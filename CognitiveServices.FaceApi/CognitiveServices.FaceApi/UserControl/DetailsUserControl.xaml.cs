using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using CognitiveServices.FaceApi.Models;
using CognitiveServices.FaceApi.Models.Services;
using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.UserControl
{
    public sealed partial class DetailsUserControl : Windows.UI.Xaml.Controls.UserControl
    {
        public static readonly DependencyProperty PersonProperty = DependencyProperty.Register
        (
            "Person",
            typeof(string),
            typeof(Person),
            new PropertyMetadata(null)
        );

        public Person Person
        {
            get => (Person) GetValue(PersonProperty);
            set
            {
                SetValue(PersonProperty, value);
                SetPerson();
            }
        }

        public static readonly DependencyProperty FaceDetectProperty = DependencyProperty.Register
        (
            "FaceDetect",
            typeof(string),
            typeof(FaceDetect),
            new PropertyMetadata(null)
        );

        public FaceDetect FaceDetect
        {
            get => (FaceDetect) GetValue(FaceDetectProperty);
            set
            {
                SetValue(FaceDetectProperty, value);
                SetFaceDetect();
            }
        }

        public static readonly DependencyProperty CandidateProperty = DependencyProperty.Register
        (
            "Candidate",
            typeof(string),
            typeof(Candidate),
            new PropertyMetadata(null)
        );

        public Candidate Candidate
        {
            get => (Candidate) GetValue(CandidateProperty);
            set
            {
                SetValue(CandidateProperty, value);
                SetCandidate();
            }
        }

        public DetailsUserControl()
        {
            InitializeComponent();
        }

        private void SetPerson()
        {
            TbkName.Text = Person.Name;
        }

        private void SetCandidate()
        {
            TbkCandidateConfidence.Text = Candidate.Confidence.ToString("N");
        }

        private void SetFaceDetect()
        {
            var attributes = FaceDetect.FaceAttributes;
            TbkAge.Text = attributes.Age.ToString("###");
            TbkGender.Text = attributes.Gender;
            TbkSmile.Text = attributes.Smile.ToString("N1");
            TbkGlasses.Text = attributes.Glasses;

            #region Emotion

            var emotion = Emotion(attributes.Emotion);
            TbkEmotion.Text = emotion.Item1;
            TbkEmotionConfidence.Text = emotion.Item2.ToString("N1");
            #endregion

            #region Makeup
            TbkEyeMakeup.Text = attributes.Makeup.EyeMakeup.ToString();
            TbkLipMakeup.Text = attributes.Makeup.LipMakeup.ToString();
            #endregion

            #region FacialHair
            TbkMoustache.Text = attributes.FacialHair.Moustache.ToString("N1");
            TbkBeard.Text = attributes.FacialHair.Moustache.ToString("N1");
            TbkSideburns.Text = attributes.FacialHair.Moustache.ToString("N1");
            #endregion

            #region Hair
            TbkBald.Text = attributes.Hair.Bald.ToString("F1");
            TbkInvisible.Text = attributes.Hair.Invisible.ToString();
            var hairColor = attributes.Hair.HairColor.OrderByDescending(x => x.Confidence).First();
            TbkColor.Text = hairColor.Color;
            TbkHairColorConfidence.Text = hairColor.Confidence.ToString("N1");
            #endregion

            TbkJson.Text = JsonConvert.SerializeObject(FaceDetect, Formatting.Indented);
        }

        private Tuple<string, double> Emotion(Emotion emotion)
        {
            var emotionDictionary = new Dictionary<string, double>();
            foreach (var prop in typeof(Emotion).GetProperties())
            {
                var name = prop.Name;
                var value = emotion.GetType().GetProperty(name).GetValue(emotion, null);
                emotionDictionary.Add(name, double.Parse(value.ToString()));
            }

            var result = emotionDictionary.OrderByDescending(x => x.Value).First();
            return new Tuple<string, double>(result.Key, result.Value);
        }
    }
}
