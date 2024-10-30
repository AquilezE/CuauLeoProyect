using Cliente.Pantallas;
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
    /// Lógica de interacción para BlockedUsersList.xaml
    /// </summary>
    public partial class BlockedUsersList : UserControl
    {
        private ObservableCollection<Cliente.Blocked> _blockedList;
        public BlockedUsersList()
        {
            InitializeComponent();
            _blockedList = Social.Instance.blockedUsersList;
            BlockedUsersListBox.ItemsSource = _blockedList;
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }
    }
}
