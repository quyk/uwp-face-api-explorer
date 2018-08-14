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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupPage : Page
    {
        private Group _person;
        private readonly GroupDialog _groupDialog;

        public GroupPage()
        {
            _groupDialog = new GroupDialog();
            InitializeComponent();
        }

        private async Task LoadPerson()
        {
            Busy(true);
            var apiService = await new ApiService().GetPersonGroups();
            if (apiService.Any())
            {
                LvGroupPerson.ItemsSource = null;
                LvGroupPerson.Items?.Clear();
                LvGroupPerson.ItemsSource = apiService;
            }
            Busy(false);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await LoadPerson();
        }

        private async void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {            
            await LoadPerson();
        }

        private void Busy(bool busy)
        {
            if (busy)
            {
                LvGroupPerson.Visibility = Visibility.Collapsed;
                Loading.Visibility = Visibility.Visible;
            }
            else
            {
                LvGroupPerson.Visibility = Visibility.Visible;
                Loading.Visibility = Visibility.Collapsed;
            }
        }

        private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = await _groupDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var name = _groupDialog.Name;
                var userData = _groupDialog.UserData;

                if (_person == null)
                {
                    var person = new Group()
                    {
                        PersonGroupId = Guid.NewGuid().ToString(),
                        Name = name,
                        UserData = userData
                    };
                    if (await new ApiService().CreatePersonGroup(person))
                    {
                        await LoadPerson();
                    }
                }
                else
                {
                    _person.Name = name;
                    _person.UserData = userData;
                    if(await new ApiService().UpdatePersonGroup(_person))
                    {
                        await LoadPerson();
                    }
                }

                Clear();
            }
        }

        private void MenuFlyoutItemEditar_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem menuFlyoutItem)
            {
                if (menuFlyoutItem.DataContext is Group person)
                {
                    _groupDialog.Name = person.Name;
                    _groupDialog.UserData = person.UserData;
                    _person = person;
                }
            }
            AddButton_OnClick(sender, e);
        }

        private async void MenuFlyoutItemExcluir_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem menuFlyoutItem)
            {
                if (menuFlyoutItem.DataContext is Group person)
                {
                    if (await new ApiService().DeletePersonGroup(person.PersonGroupId))
                    {
                        await LoadPerson();
                    }
                }
            }
        }

        private void Clear()
        {
            _person = null;
            _groupDialog.ClearFields();
        }
    }
}
