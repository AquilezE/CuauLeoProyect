using Cliente.UserControllers.FriendsList;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.Pantallas
{

    public partial class Friends : UserControl
    {

        public Friends()
        {
            InitializeComponent();
        }

        private void btFriendsList_Click(object sender, RoutedEventArgs e)
        {
            var main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new FriendList());
        }

        private void btFriendRequests_Click(object sender, RoutedEventArgs e)
        {
            var main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new FriendRequests());
        }

        private void btAddFriend_Click(object sender, RoutedEventArgs e)
        {
            var main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new AddFriend());
        }

        private void btBlockList_Click(object sender, RoutedEventArgs e)
        {
            var main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new BlockedUsersList());
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }

    }

}