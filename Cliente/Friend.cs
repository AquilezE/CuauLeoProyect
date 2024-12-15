using Cliente.ServiceReference;
using System.ComponentModel;


namespace Cliente
{
    public class Friend : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isConnected;

        public string FriendName { get; set; }
        public int FriendShipId { get; set; }
        public int FriendId { get; set; }
        public string ProfilePicturePath { get; set; }

        public bool IsConnected
        {
            get => isConnected;
            set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    OnPropertyChanged(nameof(IsConnected));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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