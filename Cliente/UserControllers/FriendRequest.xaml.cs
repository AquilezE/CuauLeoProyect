using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.UserControllers
{
    /// <summary>
    /// Lógica de interacción para FriendRequest.xaml
    /// </summary>
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
