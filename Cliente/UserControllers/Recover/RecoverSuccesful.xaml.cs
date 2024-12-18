using System.Windows;
using System.Windows.Controls;
using Cliente.Pantallas;

namespace Cliente.UserControllers.Recover
{

    public partial class RecoverSuccesful : UserControl
    {

        public RecoverSuccesful()
        {
            InitializeComponent();
        }

        public void btGotoLogIn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new LogIn());
        }

    }

}