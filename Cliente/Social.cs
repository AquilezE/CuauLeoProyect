using Cliente.ServiceReference;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.ServiceModel;
using Cliente.UserControllers;
using Haley.Utils;
using Cliente.Pantallas;
using Cliente.Utils;

namespace Cliente
{

    public class Social : ISocialManagerCallback, INotifyPropertyChanged
    {

        public SocialManagerClient SocialManagerClient;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Friend> _friendList;
        private ObservableCollection<FriendRequest> _friendRequests;
        private ObservableCollection<Blocked> _blockedUsersList;

        public static Social instance;

        public Social()
        {
            SocialManagerClient = new SocialManagerClient(new InstanceContext(this));

            var clientChannel = (ICommunicationObject)SocialManagerClient;
            clientChannel.Closed += ClientChannel_Closed;
            clientChannel.Faulted += ClientChannel_Faulted;


            FriendList = new ObservableCollection<Friend>();
            FriendRequests = new ObservableCollection<FriendRequest>();
            BlockedUsersList = new ObservableCollection<Blocked>();
        }

        private void ClientChannel_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("CLOSED");
            HandleChannelTermination();
        }

        private void ClientChannel_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("FAULTED");
            HandleChannelTermination();
        }

        private void HandleChannelTermination()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new LogIn());
            });
            Console.WriteLine("Mamaste");
            SocialManagerClient.Abort();
            SocialManagerClient = null;

            instance = null;
        }


        public ObservableCollection<Friend> FriendList
        {
            get => _friendList;
            set
            {
                _friendList = value;
                OnPropertyChanged(nameof(FriendList));
            }
        }

        public ObservableCollection<FriendRequest> FriendRequests
        {
            get => _friendRequests;
            set
            {
                _friendRequests = value;
                OnPropertyChanged(nameof(FriendRequests));
            }
        }

        public ObservableCollection<Blocked> BlockedUsersList
        {
            get => _blockedUsersList;
            set
            {
                _blockedUsersList = value;
                OnPropertyChanged(nameof(BlockedUsersList));
            }
        }


        public static Social Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Social();
                }

                return instance;
            }
            set => instance = value;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnFriendOnline(int friendId)
        {
            Console.WriteLine("Friend online" + friendId);
            Application.Current.Dispatcher.Invoke(() =>
            {
                Friend friend = FriendList.FirstOrDefault(f => f.FriendId == friendId);
                if (friend != null)
                {
                    friend.IsConnected = true;
                }
            });
        }

        public void OnFriendOffline(int friendId)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Friend friend = FriendList.FirstOrDefault(f => f.FriendId == friendId);
                if (friend != null)
                {
                    friend.IsConnected = false;
                }
            });
        }


        public void OnNewFriendRequest(FriendRequestDTO friendRequestDto)
        {
            var newFriendRequest = new FriendRequest(friendRequestDto);
            FriendRequests.Add(newFriendRequest);
            var notification = new NotificationDialog();

            notification.ShowInfoNotification(LangUtils.Translate("lblNotificationTheUser") +
                                              friendRequestDto.SenderName +
                                              LangUtils.Translate("lblNotificationSentYouFR"));
        }

        public void OnNewFriend(FriendDTO friendDto)
        {
            var newFriend = new Friend(friendDto);
            FriendList.Add(newFriend);
            FriendRequests.Remove(FriendRequests.FirstOrDefault(f => f.SenderId == friendDto.FriendId));
            var notification = new NotificationDialog();

            notification.ShowSuccessNotification(
                LangUtils.Translate("lblNotificationNowFriends") + friendDto.FriendName);
        }

        public void NotifyGameInvited(string inviterName, int lobbyId)
        {
            var notification = new NotificationDialog();

            notification.ShowInfoNotification(inviterName + LangUtils.Translate("lblNotificationInvitedToLobby") +
                                              lobbyId);
        }

        public void OnFriendshipDeleted(int userId)
        {
            Friend friend = FriendList.FirstOrDefault(f => f.FriendId == userId);
            if (friend != null)
            {
                FriendList.Remove(friend);
            }
        }

        public void Logout()
        {
            try
            {
                if (SocialManagerClient != null)
                {
                    try
                    {
                        SocialManagerClient.Disconnect(User.Instance.ID);
                    }
                    catch (EndpointNotFoundException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                        HandleChannelTermination();
                    }
                    catch (TimeoutException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
                        HandleChannelTermination();
                    }
                    catch (CommunicationException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                        HandleChannelTermination();
                    }

                    if (SocialManagerClient.State == CommunicationState.Faulted)
                    {
                        SocialManagerClient.Abort();
                    }
                    else
                    {
                        SocialManagerClient.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                SocialManagerClient?.Abort();
            }
            finally
            {
                SocialManagerClient = null;
            }

            FriendList.Clear();
            FriendRequests.Clear();
            BlockedUsersList.Clear();
        }


        public void GetFriends()
        {
            try
            {
                FriendDTO[] friends = SocialManagerClient.GetFriends(User.Instance.ID);
                foreach (FriendDTO friend in friends)
                {
                    Console.WriteLine(friend.FriendName);
                    FriendList.Add(new Friend(friend));
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
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
        }


        public void GetFriendRequests()
        {
            try
            {
                FriendRequestDTO[] friendRequests = SocialManagerClient.GetFriendRequests(User.Instance.ID);
                foreach (FriendRequestDTO friendRequest in friendRequests)
                {
                    Console.WriteLine(friendRequest.SenderName);
                    FriendRequests.Add(new FriendRequest(friendRequest));
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
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
        }

        public async void GetBlockedUsers()
        {
            try
            {
                BlockedDTO[] blockedUsers = await SocialManagerClient.GetBlockedUsersAsync(User.Instance.ID);
                foreach (BlockedDTO blockedUser in blockedUsers)
                {
                    Console.WriteLine(blockedUser.BlockerUsername);
                    BlockedUsersList.Add(new Blocked(blockedUser));
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
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
        }

    }

}