using Cliente.Pantallas;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers.ChangePassword
{

    public partial class ChangePasswordSuccess : UserControl
    {

        public ChangePasswordSuccess()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }

    }

}