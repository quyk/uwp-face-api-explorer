using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CognitiveServices.FaceApi.Helpers;
using CognitiveServices.FaceApi.Models;
using CognitiveServices.FaceApi.Service;

namespace CognitiveServices.FaceApi.Dialog
{
    public sealed partial class FaceDialog : ContentDialog
    {
        private readonly Group _group;
        private readonly Person _person;

        public FaceDialog(Group group, Person person)
        {
            _group = group;
            _person = person;
            this.InitializeComponent();
        }

        public string Url
        {
            get => TbxUrl.Text;
            set => TbxUrl.Text = value ?? string.Empty;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (UrlHelper.Check(Url))
            {
                args.Cancel = true;
                TbxUrl.IsEnabled = false;
                IsPrimaryButtonEnabled = false;
                var apiService = await new ApiService().AddPersonFace(_group, _person, new Face {Url = Url});
                if (!apiService)
                {
                    IsPrimaryButtonEnabled = true;
                    TbxUrl.IsEnabled = true;
                    return;
                }
            }
            else
            {
                TbkError.Visibility = Visibility.Visible;
                args.Cancel = true;
                return;
            }
            ClearFields();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ClearFields();
        }

        private void TxbUrl_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Url))
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }

        private void ClearFields()
        {
            Url = string.Empty;
            TbxUrl.IsEnabled = true;
            TbkError.Visibility = Visibility.Collapsed;
        }
    }
}
