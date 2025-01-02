using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{

    public partial class FindUserItem : UserControl
    {

        public event EventHandler<UserFound> SendFriendRequest;
        public event EventHandler<UserFound> BlockUser;

        public FindUserItem()
        {
            InitializeComponent();
        }

        private void btSendFriendRequest_Click(object sender, RoutedEventArgs e)
        {
            Button sendFriendRequestButton = (Button)sender;
            sendFriendRequestButton.IsEnabled = false;
            SendFriendRequest?.Invoke(this, DataContext as UserFound);
        }

        private void btBlock_Click(object sender, RoutedEventArgs e)
        {
            Button sendFriendRequestButton = (Button)sender;
            sendFriendRequestButton.IsEnabled = false;
            BlockUser?.Invoke(this, DataContext as UserFound);
        }

    }

}