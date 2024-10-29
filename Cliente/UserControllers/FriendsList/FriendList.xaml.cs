using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Cliente.UserControllers.FriendsList
{
    /// <summary>
    /// Lógica de interacción para FriendList.xaml
    /// </summary>
    public partial class FriendList : UserControl
    {
        private ObservableCollection<Cliente.Friend> _friends;
        public FriendList()
        {
            _friends = new ObservableCollection<Cliente.Friend>();
            InitializeComponent();
            FriendsListBox.ItemsSource = _friends;
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            _friends.Add(new Cliente.Friend(1, 1, "Yeezy", "9" ,true));
        }
    }
}
