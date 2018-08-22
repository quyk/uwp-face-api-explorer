using Windows.UI.Xaml.Controls;

namespace CognitiveServices.FaceApi.Dialog
{
    public sealed partial class PersonDialog : ContentDialog
    {
        public PersonDialog()
        {
            this.InitializeComponent();
        }

        public string Name
        {
            get => TxbName.Text;
            set => TxbName.Text = value ?? string.Empty;
        }

        public string UserData
        {
            get => TxbUserData.Text;
            set => TxbUserData.Text = value ?? string.Empty;
        }

        private void TxbName_OnTextChanged(object sender, TextChangedEventArgs e)
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
