using Cliente.Pantallas;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{

    public partial class RegisterSuccess : UserControl
    {
        public RegisterSuccess()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new LogIn());
        }
    }
}