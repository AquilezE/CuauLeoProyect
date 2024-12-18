using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;

namespace Cliente.UserControllers
{

    public partial class InviteFriends : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private int _currentLobbyId;

        public InviteFriends(int currentLobbyId)
        {
            InitializeComponent();
            DataContext = Social.Instance;
            this._currentLobbyId = currentLobbyId;
        }

        private void FriendLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is FriendToInvite friendToInviteUserControl)
            {
                friendToInviteUserControl.InviteFriend += OnFriendInvite;
            }
        }

        private void OnFriendInvite(object sender, Cliente.Friend friend)
        {
            try
            {
                Social.Instance.SocialManagerClient.InviteFriendToLobby(User.Instance.Username, friend.FriendId,
                    _currentLobbyId);
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (FaultException<BevososServerExceptions> ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
        }

    }

}