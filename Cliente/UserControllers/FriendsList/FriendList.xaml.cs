using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Cliente.UserControllers.FriendsList
{
    public partial class FriendList : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ICollectionView _friendsFiltered;
        public ICollectionView FriendsFiltered => _friendsFiltered;

        private string _filterSearchName;

        public FriendList()
        {
            InitializeComponent();
            DataContext = Social.Instance;

            _friendsFiltered = CollectionViewSource.GetDefaultView(Social.Instance.FriendList);
            _friendsFiltered.Filter = FilterFriends;


        }

        public string FilterSearchName{ get => _filterSearchName;
            set
            {
                _filterSearchName = value;
                OnPropertyChanged(nameof(FilterSearchName));
                _friendsFiltered.Refresh();
            }
        }

        private bool FilterFriends(object filteredFriend)
        {
            if (filteredFriend is Cliente.Friend friend)
            {
                if (string.IsNullOrEmpty(FilterSearchName))
                    return true;

                var filterCriteria = FilterSearchName.ToLower();
                return friend.FriendName.ToLower().Contains(filterCriteria);
            }
            return false;
        }


        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }

        private void FriendLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is Friend friendUserControl)
            {
                friendUserControl.deleteFriend += OnFriendDelete;
                friendUserControl.blockUser += OnBlockUser;
            }
        }

        private async void OnBlockUser(object sender, Cliente.Friend e)
        {

            if (e != null)
            {
                try
                {
                    bool result = await Social.Instance.socialManagerClient.BlockFriendAsync(User.Instance.ID, e.FriendId);
                    if (result)
                    {
                        Social.Instance.FriendList.Remove(e);
                        Social.Instance.GetBlockedUsers();
                    }
                    else
                    {
                        NotificationDialog notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrBlockingException"));
                    }
                }
                catch (EndpointNotFoundException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    NotificationDialog notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));

                }
                catch (FaultException<BevososServerExceptions> ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    NotificationDialog notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));

                }
                catch (CommunicationException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    NotificationDialog notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                }
                catch (TimeoutException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    NotificationDialog notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
                }
                catch (Exception ex)
                {
                    ExceptionManager.LogFatalException(ex);
                    NotificationDialog notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrBlockingException"));
                }
            }
        }

        private async void OnFriendDelete(object sender, Cliente.Friend e)
        {
            if (e != null)
            {
                try
                {
                    bool result = await Social.Instance.socialManagerClient.DeleteFriendAsync(User.Instance.ID, e.FriendId);
                    if (result)
                    {
                        Social.Instance.FriendList.Remove(e);
                    }
                    else
                    {

                        NotificationDialog notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrDeletingFriendException"));
                    }
                }
                catch (EndpointNotFoundException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    NotificationDialog notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));

                }
                catch (FaultException<BevososServerExceptions> ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    NotificationDialog notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));

                }
                catch (CommunicationException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    NotificationDialog notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                }
                catch (TimeoutException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    NotificationDialog notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
                }
                catch (Exception ex)
                {
                    ExceptionManager.LogFatalException(ex);
                    NotificationDialog notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrDeletingFriendException"));
                }
            }
        }
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
