using Cliente.Pantallas;
using Cliente.ServiceReference;
using Haley.Utils;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers.FriendsList
{
    /// <summary>
    /// Lógica de interacción para BlockedUsersList.xaml
    /// </summary>
    public partial class BlockedUsersList : UserControl
    {
        private ObservableCollection<Blocked> _blockedList;
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

        private void OnUnblockUser(object sender, Blocked e)
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
