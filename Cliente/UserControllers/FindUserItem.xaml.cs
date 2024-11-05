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
    /// Lógica de interacción para FindUserItem.xaml
    /// </summary>
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
