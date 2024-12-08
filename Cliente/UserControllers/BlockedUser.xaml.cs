using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{
    /// <summary>
    /// Lógica de interacción para BlockedUser.xaml
    /// </summary>
    public partial class BlockedUser : UserControl
    {
        public event EventHandler<Blocked> unblockUser;
        public BlockedUser()
        {
            InitializeComponent();
        }

        private void btUnblock_Click(object sender, RoutedEventArgs e)
        {
            unblockUser?.Invoke(this, DataContext as Blocked);
        }
    }
}
