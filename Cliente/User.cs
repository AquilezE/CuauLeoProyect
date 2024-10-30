using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class User
    {
        public static User instance;
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } 
        public int ProfilePictureId { get; set; }

        public User(UserDto userDto)
        {
            ID = userDto.UserId;
            Username = userDto.Username;
            Email = userDto.Email;
            ProfilePictureId = userDto.ProfilePictureId;
        }

        private User()
        {

        }

        public static User Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new User();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
    }
}
