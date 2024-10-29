using Cliente.Pantallas;
using Cliente.ServiceReference;
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
            InitializeComponent();
            FriendsListBox.ItemsSource = _friends;
            _friends = new ObservableCollection<Cliente.Friend>();

        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            foreach (Cliente.Friend friend in Social.Instance.friendList)
            {
                _friends.Add(friend);
            }
        }
    }
}
