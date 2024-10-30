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
    /// Lógica de interacción para FriendRequests.xaml
    /// </summary>
    public partial class FriendRequests : UserControl
    {
        private ObservableCollection<Cliente.FriendRequest> _friendRequests;
        public FriendRequests()
        {
            InitializeComponent();
            _friendRequests = Social.Instance.friendRequests;
            FriendRequestsListBox.ItemsSource = _friendRequests;
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }
    }
}
