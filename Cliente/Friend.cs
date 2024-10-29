using Cliente.Pantallas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    internal class Friend
    {
        public Friend(int friendshipId, int friendId, string friendName, string pfpPath, bool isConnected )
        {
            FriendShipId = friendshipId;
            FriendName = friendName;
            FriendId = friendId;
            FriendName = friendName;
            ProfilePicturePath = "pack://application:,,,/Images/pfp" + pfpPath + ".jpg";
            IsConnected = isConnected;
        }

        public string FriendName { get; set; }
        public int FriendShipId { get; set; }
        public int FriendId { get; set; }
        public string ProfilePicturePath { get; set; }
        public bool IsConnected { get; set; }
    }
}
