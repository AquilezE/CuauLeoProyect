using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{

    public partial class BlockedUser : UserControl
    {

        public event EventHandler<Blocked> UnblockUser;

        public BlockedUser()
        {
            InitializeComponent();
        }

        private void btUnblock_Click(object sender, RoutedEventArgs e)
        {
            UnblockUser?.Invoke(this, DataContext as Blocked);
        }

    }

}