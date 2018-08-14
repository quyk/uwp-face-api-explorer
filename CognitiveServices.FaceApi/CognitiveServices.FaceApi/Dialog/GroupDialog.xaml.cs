using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CognitiveServices.FaceApi.Dialog
{
    public sealed partial class GroupDialog : ContentDialog
    {
        public GroupDialog()
        {
            this.InitializeComponent();
        }

        public string Name
        {
            get => txbName.Text;
            set => txbName.Text = value ?? string.Empty;
        }

        public string UserData
        {
            get => txbUserData.Text;
            set => txbUserData.Text = value ?? string.Empty;
        }

        private void GroupName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }

        public void ClearFields()
        {
            Name = string.Empty;
            UserData = string.Empty;
        }
    }
}
