using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CognitiveServices.FaceApi.Models;
using CognitiveServices.FaceApi.Service;

namespace CognitiveServices.FaceApi.Views
{
    public sealed partial class TrainPage : Page
    {
        private Group _group;

        public TrainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await LoadGroup();
        }

        private async Task LoadGroup()
        {
            var apiService = await new ApiService().GetPersonGroups();
            if (apiService.Any())
            {
                CbxGroup.PlaceholderText = "Grupo";
                CbxGroup.ItemsSource = apiService;
                CbxGroup.IsEnabled = true;
            }
        }

        private void CbxGroup_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedValue is Group group)
                {
                    _group = group;
                    AbbPlay.IsEnabled = true;
                    AbbChecar.IsEnabled = true;
                }
            }
        }

        private async void AbbPlay_OnClick(object sender, RoutedEventArgs e)
        {
            if (_group != null)
            {
                await new ApiService().TrainPersonGroup(_group);
            }
        }

        private async void AbbChecar_OnClick(object sender, RoutedEventArgs e)
        {
            if (_group != null)
            {
                await new ApiService().GetGroupTraining(_group);
            }
        }
    }
}
