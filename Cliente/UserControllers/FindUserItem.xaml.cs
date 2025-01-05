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
            DisableButtons();
            SendFriendRequest?.Invoke(this, DataContext as UserFound);
        }

        private void btBlock_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            BlockUser?.Invoke(this, DataContext as UserFound);
        }

        private void DisableButtons()
        {
            btSendFriendRequest.IsEnabled = false;
            btBlock.IsEnabled = false;
        }

        public void EnableButtons()
        {
            btSendFriendRequest.IsEnabled = true;
            btBlock.IsEnabled = true;
        }

    }

}