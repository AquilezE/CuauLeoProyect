using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{

    public partial class FriendToInvite : UserControl
    {

        public event EventHandler<Cliente.Friend> InviteFriend;

        public FriendToInvite()
        {
            InitializeComponent();
        }

        private void btInvite_Click(object sender, RoutedEventArgs e)
        {
            InviteFriend?.Invoke(this, DataContext as Cliente.Friend);
        }

    }

}