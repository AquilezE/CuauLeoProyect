﻿using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.UserControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class FriendRequest
    {
        public int FriendRequestId { get; set; }
        public string SenderName { get; set; }
        public int SenderId { get; set; }
        public string SenderProfilePicturePath { get; set; }

        public FriendRequest(int friendRequestId, string senderName, int senderId, string senderProfilePicturePath)
        {
            FriendRequestId = friendRequestId;
            SenderName = senderName;
            SenderId = senderId;
            SenderProfilePicturePath = senderProfilePicturePath;
        }

        public FriendRequest(FriendRequestDTO friendRequestDto)
        {
            FriendRequestId = friendRequestDto.FriendRequestId;
            SenderId = friendRequestDto.SenderId;
            SenderName = friendRequestDto.SenderName;
            SenderProfilePicturePath = "pack://application:,,,/Images/pfp" + friendRequestDto.ProfilePictureId + ".jpg";
        }
    }
}
