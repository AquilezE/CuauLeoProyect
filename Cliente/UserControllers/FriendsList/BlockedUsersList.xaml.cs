using Cliente.Pantallas;
using Cliente.ServiceReference;
using Haley.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
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
            DataContext = Social.Instance;
            _blockedList = Social.Instance.BlockedUsersList;
            BlockedUsersListBox.ItemsSource = _blockedList;
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }

        private void BlockedUserLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is BlockedUser blockedUserControl)
            {
                blockedUserControl.unblockUser += OnUnblockUser;
            }
        }

        private void OnUnblockUser(object sender, Cliente.Blocked e)
        {
            if (e != null)
            {
                try
                {
                    bool result = Social.Instance.socialManagerClient.UnblockUser(User.Instance.ID, e.BlockedId);
                    if (result) {
                        Social.Instance.BlockedUsersList.Remove(e);
                    }
                    else
                    {
                        MessageBox.Show(LangUtils.Translate("lblErrUnblockingException"));
                    }
                }catch(CommunicationException ex)
                {
                    MessageBox.Show(LangUtils.Translate("lblErrNoConection"));
                }
            }
        }

        public void OnFriendNew(FriendDTO[] friends)
        {
            //delete
            throw new NotImplementedException();
        }
    }
}
