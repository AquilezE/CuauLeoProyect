using Servicio.Interfaces;
using Servicio.Clases;
using Servicio.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio
{
    public class LogInService : ILogIn
    {

        private PasswordHasher _passwordHasher = new PasswordHasher();
        private string _passwordTestHash;
        private LogInCredentials logInCredentials;
        private User testUser;

        public LogInService()
        {
            _passwordTestHash = _passwordHasher.HashPassword("kekistan");

            logInCredentials = new LogInCredentials()
            {
                Username = "papoi",
                Password = _passwordTestHash
            };

            testUser = new User()
            {
                ID = 1,
                Username = "papoi",
                Email = "papoi@gmail.com"
            };
        }

        public User TryLogIn(string user, string password)
        {
                        
            if (user == logInCredentials.Username && _passwordHasher.VerifyPassword( password, logInCredentials.Password))
            {
                return testUser;
            }
            else
            {
                return null;
            }
        }
    }
}
