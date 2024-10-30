using Cliente.UserControllers.FriendsList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.Pantallas
{
    /// <summary>
    /// Lógica de interacción para Friends.xaml
    /// </summary>
    public partial class Friends : UserControl
    {
        public Friends()
        {
            InitializeComponent();
        }

        private void btFriendsList_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new FriendList());
        }

        private void btFriendRequests_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new Cliente.UserControllers.FriendsList.FriendRequests());
        }

        private void btAddFriend_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btBlockList_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new Cliente.UserControllers.FriendsList.BlockedUsersList());
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }
    }
}
