using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CognitiveServices.FaceApi.Helpers;

namespace CognitiveServices.FaceApi.Dialog
{
    public sealed partial class ImageDialog : ContentDialog
    {
        public ImageDialog()
        {
            this.InitializeComponent();
        }

        public string Image
        {
            get => TxbImage.Text;
            set => TxbImage.Text = value ?? string.Empty;
        }

        public void Clear()
        {
            Image = string.Empty;
            TbkError.Visibility = Visibility.Collapsed;
        }

        private void TxbImage_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Image))
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }

        private void ImageDialog_OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (UrlHelper.Check(Image))
            {
                TbkError.Visibility = Visibility.Collapsed;
            }
            else
            {
                TbkError.Visibility = Visibility.Visible;
                args.Cancel = true;
            }
        }

        private void ImageDialog_OnSecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Clear();
        }
    }
}
