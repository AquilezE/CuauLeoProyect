using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{

    public partial class FriendRequest : UserControl
    {

        public event EventHandler<Cliente.FriendRequest> AcceptFriend;
        public event EventHandler<Cliente.FriendRequest> DeclineFriend;

        public FriendRequest()
        {
            InitializeComponent();
        }

        private void btAccept_Click(object sender, RoutedEventArgs e)
        {
            AcceptFriend?.Invoke(this, DataContext as Cliente.FriendRequest);
        }

        private void btDecline_Click(object sender, RoutedEventArgs e)
        {
            DeclineFriend?.Invoke(this, DataContext as Cliente.FriendRequest);
        }

    }

}