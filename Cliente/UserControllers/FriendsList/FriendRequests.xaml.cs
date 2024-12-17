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
                friendRequestUserControl.acceptFriend += OnFriendRequestAccept;
                friendRequestUserControl.declineFriend += OnFriendRequestDecline;
            }
        }

        private async void OnFriendRequestAccept(object sender, Cliente.FriendRequest e)
        {
            if (e != null)
            {
                try
                {
                    bool result =
                        await Social.Instance.socialManagerClient.AcceptFriendRequestAsync(User.Instance.ID, e.SenderId,
                            e.FriendRequestId);
                    if (result)
                        Social.Instance.FriendRequests.Remove(e);
                    else
                    {
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrAcceptingFRException"));
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
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
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
                        await Social.Instance.socialManagerClient.DeclineFriendRequestAsync(e.FriendRequestId);

                    if (result)
                        Social.Instance.FriendRequests.Remove(e);
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
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
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