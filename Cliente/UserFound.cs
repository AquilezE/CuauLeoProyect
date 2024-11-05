using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class UserFound
    {
        public int ID { get; set; }
        public string UserFoundUsername { get; set; }
        public string UserFoundProfilePicturePath { get; set; }

        public UserFound(int id, string username, string profilePictureId)
        {
            ID = id;
            UserFoundUsername = username;
            UserFoundProfilePicturePath = profilePictureId;
        }

        public UserFound(UserDto userDto)
        {
            ID = userDto.UserId;
            UserFoundUsername = userDto.Username;
            UserFoundProfilePicturePath = "pack://application:,,,/Images/pfp" + userDto.ProfilePictureId + ".jpg";
        }
    }
}
