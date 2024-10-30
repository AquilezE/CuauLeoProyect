﻿using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    internal class Social: ISocialManagerCallback
    {
        private static SocialManagerClient socialManagerClient;

        public static Social instance;
        public ObservableCollection<Friend> friendList { get; set; }
        public ObservableCollection<FriendRequest> friendRequests { get; set; }
        public ObservableCollection<Blocked> blockedUsersList { get; set; }

        public Social()
        {
            socialManagerClient = new SocialManagerClient(new System.ServiceModel.InstanceContext(this));
            friendList = new ObservableCollection<Friend>();
            friendRequests = new ObservableCollection<FriendRequest>();
            blockedUsersList = new ObservableCollection<Blocked>();
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

        public void GetFriends()
        {
            FriendDTO[] friends = socialManagerClient.GetFriends(User.Instance.ID);
            foreach (FriendDTO friend in friends)
            {
                Console.WriteLine(friend.FriendName);
                friendList.Add(new Friend(friend));
            }

        }

        public void GetFriendRequests()
        {
            FriendRequestDTO[] friendRequests = socialManagerClient.GetFriendRequests(User.Instance.ID);
            foreach (FriendRequestDTO friendRequest in friendRequests)
            {
                Console.WriteLine(friendRequest.SenderName);
                this.friendRequests.Add(new FriendRequest(friendRequest));
            }
        }

        public void GetBlockedUsers()
        {
            BlockedDTO[] blockedUsers = socialManagerClient.GetBlockedUsers(User.Instance.ID);
            foreach (BlockedDTO blockedUser in blockedUsers)
            {
                Console.WriteLine(blockedUser.BlockerUsername);
                blockedUsersList.Add(new Blocked(blockedUser));
            }
        }

        public void OnFriendNew(FriendDTO[] friends)
        {
            throw new NotImplementedException();
        }
    }
}
