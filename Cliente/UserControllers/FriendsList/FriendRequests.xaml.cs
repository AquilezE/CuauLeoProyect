using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers.FriendsList
{

    public partial class FriendRequests : UserControl
    {

        private ObservableCollection<Cliente.FriendRequest> _friendRequests;

        public FriendRequests()
        {
            InitializeComponent();
            DataContext = Social.Instance;
            _friendRequests = Social.Instance.FriendRequests;
            FriendRequestsListBox.ItemsSource = _friendRequests;
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }

        private void FriendRequestLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is FriendRequest friendRequestUserControl)
            {
                var friendRequest = friendRequestUserControl.DataContext as Cliente.FriendRequest;

                bool isBlocked = Social.Instance.BlockedUsersList.Any(b => b.BlockedId == friendRequest.SenderId);
                
                friendRequestUserControl.btAcceptFriendRequest.IsEnabled = !isBlocked ;
                
                friendRequestUserControl.AcceptFriend += OnFriendRequestAccept;
                friendRequestUserControl.DeclineFriend += OnFriendRequestDecline;
            }
        }

        private async void OnFriendRequestAccept(object sender, Cliente.FriendRequest e)
        {
            if (e != null)
            {
                try
                {
                    bool result =
                        await Social.Instance.SocialManagerClient.AcceptFriendRequestAsync(User.Instance.ID, e.SenderId,
                            e.FriendRequestId);
                    if (result)
                    {
                        Social.Instance.FriendRequests.Remove(e);
                    }
                    else
                    {
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrAcceptingFRException"));
                        Social.Instance.FriendRequests.Remove(e);
                    }
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
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrSocialRequestTimeout"));
                }
                catch (Exception ex)
                {
                    ExceptionManager.LogFatalException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrAcceptingFRException"));
                }
            }
        }

        private async void OnFriendRequestDecline(object sender, Cliente.FriendRequest e)
        {
            if (e != null)
            {
                try
                {
                    bool result =
                        await Social.Instance.SocialManagerClient.DeclineFriendRequestAsync(User.Instance.ID, e.FriendRequestId);

                    if (result)
                    {
                        Social.Instance.FriendRequests.Remove(e);
                    }
                    else
                    {
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrDecliningFRException"));
                    }
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
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrSocialRequestTimeout"));
                }
                catch (Exception ex)
                {
                    ExceptionManager.LogFatalException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrDecliningFRException"));
                }
            }
        }

    }

}