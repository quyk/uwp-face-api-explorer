using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CognitiveServices.FaceApi.Dialog;
using CognitiveServices.FaceApi.Models;
using CognitiveServices.FaceApi.Service;

namespace CognitiveServices.FaceApi.Views
{
    public sealed partial class FacePage : Page
    {
        private Group _group;
        private Person _person;
        private readonly IList<Face> _faces;

        public FacePage()
        {
            _faces = new List<Face>();
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                var parameter = (Parameter) e.Parameter;
                 _group = parameter.Group;
                _person = parameter.Person;
                await LoadFace();
            }
            base.OnNavigatedTo(e);
        }


        private async Task LoadFace()
        {
            Busy(true);
            if (_person != null)
            {
                _faces.Clear();
                foreach (var personPersistedFacesId in _person.PersistedFacesIds)
                {
                    var apiService = await new ApiService().GetPersonFace(_group, _person, personPersistedFacesId);
                    if (apiService != null)
                    {
                        apiService.Name = _person.Name;
                        _faces.Add(apiService);
                    }
                }

                if (_faces.Any())
                {
                    LvwFace.ItemsSource = null;
                    LvwFace.Items?.Clear();
                    LvwFace.ItemsSource = _faces;
                }
            }
            Busy(false);
        }

        private async Task LoadPerson()
        {
            _person = await new ApiService().GetPerson(_group, _person);
        }

        private void Busy(bool busy)
        {
            if (busy)
            {
                LvwFace.Visibility = Visibility.Collapsed;
                Loading.Visibility = Visibility.Visible;
            }
            else
            {
                LvwFace.Visibility = Visibility.Visible;
                Loading.Visibility = Visibility.Collapsed;
            }
        }

        private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = await new FaceDialog(_group, _person).ShowAsync();
            if (dialog == ContentDialogResult.Secondary)
            {
                await LoadPerson();
                await LoadFace();
            }
        }

        private async void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            await LoadFace();
        }

        private async void MenuFlyoutItemExcluir_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem menuFlyoutItem)
            {
                if (menuFlyoutItem.DataContext is Face face)
                {
                    if (await new ApiService().DeletePersonFace(_group, _person, face))
                    {
                        await LoadPerson();
                        await LoadFace();
                    }
                }
            }
        }
    }
}
