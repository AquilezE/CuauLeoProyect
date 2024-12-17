using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{

    public partial class FriendRequest : UserControl
    {
        public event EventHandler<Cliente.FriendRequest> acceptFriend;
        public event EventHandler<Cliente.FriendRequest> declineFriend;

        public FriendRequest()
        {
            InitializeComponent();
        }

        private void btAccept_Click(object sender, RoutedEventArgs e)
        {
            acceptFriend?.Invoke(this, DataContext as Cliente.FriendRequest);
        }

        private void btDecline_Click(object sender, RoutedEventArgs e)
        {
            declineFriend?.Invoke(this, DataContext as Cliente.FriendRequest);
        }
    }
}