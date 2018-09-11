using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using CognitiveServices.FaceApi.Dialog;
using CognitiveServices.FaceApi.Models;
using CognitiveServices.FaceApi.Service;

namespace CognitiveServices.FaceApi.Views
{
    public sealed partial class AnalyzePage : Page
    {
        private Group _group;
        private string _image;
        private IList<string> _faces;
        private Identify _identify;
        private readonly ImageDialog _imageDialog;

        public AnalyzePage()
        {
            _imageDialog = new ImageDialog();
            _identify = new Identify();
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await LoadGroup();
        }

        private async void AbbImage_OnClick(object sender, RoutedEventArgs e)
        {
            var result = await _imageDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                _image = _imageDialog.Image;
                var image = new BitmapImage(new Uri(_image, UriKind.Absolute));
                image.ImageOpened += (o, args) =>
                {
                    GrdImage.Width = image.PixelWidth;
                    GrdImage.Height = image.PixelHeight;
                };
                ImgFace.Source = image;
                
                _imageDialog.Clear();
                CleanCanvas();
                AbbDetect.IsEnabled = true;
                AbbIdentify.IsEnabled = false;
            }
        }

        private async void AbbDetect_OnClick(object sender, RoutedEventArgs e)
        {
            CleanCanvas();
            var service = await new ApiService().Detect(_image);
            if (service.Any())
            {
                _faces = new List<string>();
                foreach (var face in service)
                {
                    var rectangle = Draw(face);
                    Canvas.SetTop(rectangle, face.FaceRectangle.Top);
                    Canvas.SetLeft(rectangle, face.FaceRectangle.Left);
                    GrdImage.Children.Add(rectangle);
                    _faces.Add(face.FaceId);
                }
            }
            AbbIdentify.IsEnabled = true;
        }

        private async void AbbIdentify_OnClick(object sender, RoutedEventArgs e)
        {
            _identify.PersonGroupId = _group.PersonGroupId;
            _identify.ConfidenceThreshold = 0.4;
            _identify.FaceIds = _faces;

            if (_identify != null)
            {
                var identifyResponse = await new ApiService().Identify(_identify);
                if (identifyResponse.Any())
                {
                    foreach (var identify in identifyResponse)
                    {
                        if (identify.Candidates.Any())
                        {
                            foreach (var candidate in identify.Candidates)
                            {
                                var person = await new ApiService().GetPersonFaces(_group, candidate.PersonId);
                                DrawGreen(person, identify.FaceId);
                            }
                        }
                    }
                }
            }
        }

        private void Busy(bool busy)
        {
            if (busy)
            {
                SvrImage.Visibility = Visibility.Collapsed;
                Loading.Visibility = Visibility.Visible;
            }
            else
            {
                SvrImage.Visibility = Visibility.Visible;
                Loading.Visibility = Visibility.Collapsed;
            }
        }

        private async Task LoadGroup()
        {
            Busy(true);
            var apiService = await new ApiService().GetPersonGroups();
            if (apiService.Any())
            {
                CbxGroup.PlaceholderText = "Grupo";
                CbxGroup.ItemsSource = apiService;
                CbxGroup.IsEnabled = true;
            }
            Busy(false);
        }

        private async void CbxGroup_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedValue is Group group)
                {
                    _group = group;
                    AbbImage.IsEnabled = true;
                    await LoadGroup();
                }
            }
        }

        private static Rectangle Draw(FaceDetect face)
        {
            return new Rectangle
            {
                Width = face.FaceRectangle.Width,
                Height = face.FaceRectangle.Height,
                Stroke = new SolidColorBrush(Colors.Yellow),
                Fill = new SolidColorBrush(Colors.Transparent),
                StrokeThickness = 3,
                Name = face.FaceId
            };
        }

        private void DrawGreen(Person person, string faceId)
        {
            var rectangle = GrdImage.Children.OfType<Rectangle>().First(x => x.Name == faceId);
            if (rectangle != null)
            {
                rectangle.Stroke = new SolidColorBrush(Colors.DarkGreen);
                rectangle.Tapped += async (sender, args) =>
                {
                    await new MessageDialog($"Person: {person.Name}").ShowAsync();
                };
            }
        }

        private void CleanCanvas()
        {
            var rectangles = GrdImage.Children.OfType<Rectangle>().ToList();
            foreach (var rectangle in rectangles)
            {
                GrdImage.Children.Remove(rectangle);
            }
        }
    }
}
