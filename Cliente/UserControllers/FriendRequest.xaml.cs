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
            DisableButtons();
            AcceptFriend?.Invoke(this, DataContext as Cliente.FriendRequest);
        }

        private void btDecline_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            DeclineFriend?.Invoke(this, DataContext as Cliente.FriendRequest);
        }

        private void DisableButtons()
        {
            btAccept.IsEnabled = false;
            btDecline.IsEnabled = false;
        }

        public void EnableButtons()
        {
            btAccept.IsEnabled = true;
            btDecline.IsEnabled = true;
        }

    }

}