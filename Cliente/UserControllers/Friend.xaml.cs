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
