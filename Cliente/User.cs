using Cliente.ServiceReference;

namespace Cliente
{
    public class User
    {
        public static User instance;
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } 
        public int ProfilePictureId { get; set; }

        public User(UserDTO userDto)
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
