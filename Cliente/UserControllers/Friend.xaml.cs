using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{
    /// <summary>
    /// Lógica de interacción para Friend.xaml
    /// </summary>
    public partial class Friend : UserControl
    {
        public event EventHandler<Cliente.Friend> deleteFriend;
        public event EventHandler<Cliente.Friend> blockUser;
        public Friend()
        {
            InitializeComponent();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            deleteFriend?.Invoke(this, DataContext as Cliente.Friend);
        }

        private void btBlockUser_Click(object sender, RoutedEventArgs e)
        {
            blockUser?.Invoke(this, DataContext as Cliente.Friend);
        }
    }
}
