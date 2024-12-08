using Cliente.Pantallas;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers.ChangePassword
{
    /// <summary>
    /// Lógica de interacción para ChangePasswordSuccess.xaml
    /// </summary>
    public partial class ChangePasswordSuccess : UserControl
    {
        public ChangePasswordSuccess()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }
    }
}
