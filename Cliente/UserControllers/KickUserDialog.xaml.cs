using Haley.Utils;
using System.Windows;

namespace Cliente.UserControllers
{

    public partial class KickUserDialog : Window
    {
        public string KickReason { get; private set; }

        public KickUserDialog(string username)
        {
            InitializeComponent();

            Title = LangUtils.Translate("lblKicking") + $" {username}";
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btKick_Click(object sender, RoutedEventArgs e)
        {
            KickReason = tbReason.Text.Trim();

            if (string.IsNullOrEmpty(KickReason))
            {
                MessageBox.Show(LangUtils.Translate("lblTypeReason"), "???", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}