using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{

    public partial class Friend : UserControl
    {

        public event EventHandler<Cliente.Friend> DeleteFriend;
        public event EventHandler<Cliente.Friend> BlockUser;

        public Friend()
        {
            InitializeComponent();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            DeleteFriend?.Invoke(this, DataContext as Cliente.Friend);
        }

        private void btBlockUser_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            BlockUser?.Invoke(this, DataContext as Cliente.Friend);
        }

        private void DisableButtons()
        {
            btDelete.IsEnabled = false;
            btBlockUser.IsEnabled = false;
        }

        public void EnableButtons()
        {
            btDelete.IsEnabled = true;
            btBlockUser.IsEnabled = true;
        }

    }

}