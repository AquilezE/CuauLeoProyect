using Cliente.Pantallas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cliente.ServiceReference;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cliente
{
    public class Friend
    {

        public string FriendName { get; set; }
        public int FriendShipId { get; set; }
        public int FriendId { get; set; }
        public string ProfilePicturePath { get; set; }
        public bool IsConnected { get; set; }

        public Friend(int friendshipId, int friendId, string friendName, string profilePicturePath, bool isConnected)
        {
            FriendShipId = friendshipId;
            FriendId = friendId;
            FriendName = friendName;
            ProfilePicturePath = profilePicturePath;
            IsConnected = isConnected;
        }

        public Friend(FriendDTO friendDto)
        {
            FriendShipId = friendDto.FriendshipId;
            FriendId = friendDto.FriendId;
            FriendName = friendDto.FriendName;
            ProfilePicturePath = "pack://application:,,,/Images/pfp" + friendDto.ProfilePictureId + ".jpg";
            IsConnected = friendDto.IsConnected;
        }
    }
}

