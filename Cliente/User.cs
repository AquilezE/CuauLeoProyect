using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    internal class User
    {
        public static User instance;
        public int ID;
        public string Username;
        public string Email;

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
        }
    }
}
