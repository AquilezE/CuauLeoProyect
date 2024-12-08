using Cliente.ServiceReference;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cliente
{
    public class UserLobby: INotifyPropertyChanged
    {

       public event PropertyChangedEventHandler PropertyChanged;
       protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

       private bool _isReady;

       public int ID { get; set; }
       public string Username { get; set; }

       public string Email { get; set; }

       public int ProfilePictureId { get; set; }

        public string ProfilePicturePath
        {
            get
            {
                return "pack://application:,,,/Images/pfp" + ProfilePictureId + ".jpg";
            }
        }

        public bool IsReady
        {
            get => _isReady;
            set
            {
                if (_isReady != value)
                {
                    _isReady = value;
                    OnPropertyChanged();
                }
            }
        }

        public UserLobby(UserDTO userDto)
        {
            ID = userDto.UserId;
            Username = userDto.Username;
            Email = userDto.Email;
            ProfilePictureId = userDto.ProfilePictureId;
            IsReady = userDto.IsReady;
        }

        public UserLobby(User user)
        {
            ID = user.ID;
            Username = user.Username;
            Email = user.Email;
            ProfilePictureId = user.ProfilePictureId;
            IsReady = true;
        }
    }
}
