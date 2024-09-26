using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Cliente.UserControllers;
using Cliente.Pantallas;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.UserControllers.Recover
{
    /// <summary>
    /// Interaction logic for RecoverSuccesful.xaml
    /// </summary>
    
    public partial class RecoverSuccesful : UserControl
    {

        public RecoverSuccesful()
        {
            InitializeComponent();
        }

        public void btGotoLogIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToMensajeador(new LogIn());
        }
    }
}
