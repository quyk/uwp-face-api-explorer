using System;
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
    public sealed partial class PersonPage : Page
    {
        private Group _group;
        private Person _person;
        private readonly PersonDialog _personDialog;

        public PersonPage()
        {
            _personDialog = new PersonDialog();
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

        private async Task LoadPerson()
        {
            Busy(true);
            var apiService = await new ApiService().GetPersonsByGroup(_group.PersonGroupId);
            if (apiService.Any())
            {
                LvwPerson.ItemsSource = null;
                LvwPerson.Items?.Clear();
                LvwPerson.ItemsSource = apiService;
            }
            Busy(false);
        }

        private void Busy(bool busy)
        {
            if (busy)
            {
                LvwPerson.Visibility = Visibility.Collapsed;
                Loading.Visibility = Visibility.Visible;
            }
            else
            {
                LvwPerson.Visibility = Visibility.Visible;
                Loading.Visibility = Visibility.Collapsed;
            }
        }

        private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = await _personDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var name = _personDialog.Name;
                var userData = _personDialog.UserData;

                if (_person == null)
                {
                    var person = new Person()
                    {
                        Name = name,
                        UserData = userData
                    };
                    if (await new ApiService().CreatePerson(_group, person))
                    {
                        await LoadPerson();
                    }
                }
                else
                {
                    _person.Name = name;
                    _person.UserData = userData;

                    if (await new ApiService().UpdatePerson(_group, _person))
                    {
                        await LoadPerson();
                    }
                }
                Clear();
            }
        }

        private async void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            await LoadPerson();
        }

        private async void MenuFlyoutItemExcluir_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem menuFlyoutItem)
            {

                if (menuFlyoutItem.DataContext is Person person)
                {
                    if (await new ApiService().DeletePerson(_group, person))
                    {
                        await LoadPerson();
                    }
                }
            }
        }

        private void MenuFlyoutItemEditar_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem menuFlyoutItem)
            {
                if (menuFlyoutItem.DataContext is Person person)
                {
                    _personDialog.Name = person.Name;
                    _personDialog.UserData = person.UserData;
                    _person = person;
                }
            }

            AddButton_OnClick(sender, e);
        }

        private void MenuFlyoutItemUpload_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void CbxGroup_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedValue is Group group)
                {
                    _group = group;
                    AbbAdd.IsEnabled = true;
                    AbbRefresh.IsEnabled = true;
                    await LoadPerson();
                }
            }
        }

        private void Clear()
        {
            _person = null;
            _personDialog.ClearFields();
        }
    }
}
