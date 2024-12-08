﻿using Cliente.ServiceReference;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.ServiceModel;
using Cliente.UserControllers;
using Haley.Utils;
using Cliente.Pantallas;

namespace Cliente
{
    public class Social: ISocialManagerCallback, INotifyPropertyChanged
    {
        public SocialManagerClient socialManagerClient;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Friend> _friendList;
        private ObservableCollection<FriendRequest> _friendRequests;
        private ObservableCollection<Blocked> _blockedUsersList;

        public static Social instance;

        public Social()
        {
            socialManagerClient = new SocialManagerClient(new InstanceContext(this));

            ICommunicationObject clientChannel = (ICommunicationObject)socialManagerClient;
            clientChannel.Closed += ClientChannel_Closed;
            clientChannel.Faulted += ClientChannel_Faulted;


            FriendList = new ObservableCollection<Friend>();
            FriendRequests = new ObservableCollection<FriendRequest>();
            BlockedUsersList = new ObservableCollection<Blocked>();
        }

        private void ClientChannel_Closed(object sender, EventArgs e)
        {
            HandleChannelTermination("Connection closed. Redirecting to login...");
        }

        private void ClientChannel_Faulted(object sender, EventArgs e)
        {
            HandleChannelTermination("Connection faulted. Redirecting to login...");
        }

        private void HandleChannelTermination(string message)
        {
            // Log the termination message if necessary
            Console.WriteLine(message);

            // Navigate to the login page
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new LogIn());
            });

            // Cleanup: Dispose of the current client
            socialManagerClient.Abort();
            socialManagerClient = null;

            // Optionally, reset the Social instance
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
            set
            {
                instance = value;
            }
        }

        protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void OnFriendOnline(int friendId)
        {
            Console.WriteLine("Friend online" + friendId);
            Application.Current.Dispatcher.Invoke(() =>
            {
                var friend = FriendList.FirstOrDefault(f => f.FriendId == friendId);
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
                var friend = FriendList.FirstOrDefault(f => f.FriendId == friendId);
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
            NotificationDialog notification = new NotificationDialog();

            //FALTA INTERNACIONALIZAR
            notification.ShowInfoNotification(LangUtils.Translate("lblNotificationTheUser") + friendRequestDto.SenderName + LangUtils.Translate("lblNotificationSentYouFR"));
        }

        public void OnNewFriend(FriendDTO friendDto)
        {
            var newFriend = new Friend(friendDto);
            FriendList.Add(newFriend);
            FriendRequests.Remove(FriendRequests.FirstOrDefault(f => f.SenderId == friendDto.FriendId));
            NotificationDialog notification = new NotificationDialog();

            //FALTA INTERNACIONALIZAR
            notification.ShowSuccessNotification(LangUtils.Translate("lblNotificationNowFriends") + friendDto.FriendName);
        }

        public void NotifyGameInvited(string inviterName, int lobbyId)
        {
            NotificationDialog notification = new NotificationDialog();

            notification.ShowInfoNotification(inviterName + LangUtils.Translate("lblNotificationInvitedToLobby") + lobbyId);
        }

        public void OnFriendshipDeleted(int userId)
        {
            object friend = FriendList.FirstOrDefault(f => f.FriendId == userId);
            if (friend != null)
            {
                FriendList.Remove((Friend)friend);
            }
        }

        public void Logout()
        {
            try
            {
                if (socialManagerClient != null)
                {
                    try
                    {
                        socialManagerClient.Disconnect(User.Instance.ID);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error disconnecting: {ex.Message}");
                    }

                    if (socialManagerClient.State == CommunicationState.Faulted)
                    {
                        socialManagerClient.Abort();
                    }
                    else
                    {
                        socialManagerClient.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing client: {ex.Message}");
                socialManagerClient?.Abort(); 
            }
            finally
            {
                socialManagerClient = null;
            }

            FriendList.Clear();
            FriendRequests.Clear();
            BlockedUsersList.Clear();
        }


        public void GetFriends()
        {
            FriendDTO[] friends = socialManagerClient.GetFriends(User.Instance.ID);
            foreach (FriendDTO friend in friends)
            {
                Console.WriteLine(friend.FriendName);
                FriendList.Add(new Friend(friend));
            }

        }


        public void GetFriendRequests()
        {
            FriendRequestDTO[] friendRequests = socialManagerClient.GetFriendRequests(User.Instance.ID);
            foreach (FriendRequestDTO friendRequest in friendRequests)
            {
                Console.WriteLine(friendRequest.SenderName);
                FriendRequests.Add(new FriendRequest(friendRequest));
            }
        }

        public void GetBlockedUsers()
        {
            BlockedDTO[] blockedUsers = socialManagerClient.GetBlockedUsers(User.Instance.ID);
            foreach (BlockedDTO blockedUser in blockedUsers)
            {
                Console.WriteLine(blockedUser.BlockerUsername);
                BlockedUsersList.Add(new Blocked(blockedUser));
            }
        }
    }
}
