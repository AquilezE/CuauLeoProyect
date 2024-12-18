using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{

    public partial class FindUserItem : UserControl
    {

        public event EventHandler<UserFound> sendFriendRequest;
        public event EventHandler<UserFound> blockUser;

        public FindUserItem()
        {
            InitializeComponent();
        }

        private void btSendFriendRequest_Click(object sender, RoutedEventArgs e)
        {
            sendFriendRequest?.Invoke(this, DataContext as UserFound);
        }

        private void btBlock_Click(object sender, RoutedEventArgs e)
        {
            blockUser?.Invoke(this, DataContext as UserFound);
        }

    }

}