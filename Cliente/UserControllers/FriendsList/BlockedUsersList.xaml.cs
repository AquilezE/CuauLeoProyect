using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers.FriendsList
{

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
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }

        private void BlockedUserLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is BlockedUser blockedUserControl)
            {
                blockedUserControl.UnblockUser += OnUnblockUser;
            }
        }

        private async void OnUnblockUser(object sender, Blocked e)
        {
            if (e == null)
            {
                return;
            }

            try
            {
                bool result = await Social.Instance.SocialManagerClient.UnblockUserAsync(User.Instance.ID, e.BlockedId);
                if (result)
                {
                    Social.Instance.BlockedUsersList.Remove(e);
                }
                else
                {
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrUnblockingException"));
                }
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                Social.Instance.BlockedUsersList.Remove(e);
            }
            catch (FaultException<BevososServerExceptions> ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));
                (sender as BlockedUser).EnableButtons();
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                Social.Instance.BlockedUsersList.Remove(e);
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrSocialRequestTimeout"));
                Social.Instance.BlockedUsersList.Remove(e);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrUnblockingException"));
            }
        }

    }

}