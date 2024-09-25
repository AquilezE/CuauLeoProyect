using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Utilities
{
    public class PasswordHasher
    {

        public string HashPassword(string password)
        {

            byte[] salt = GenerateSalt();


            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                // Combine salt and hash, YOU STORE THIS IN THE DB, WHEN WE GET ONE SMH 
                byte[] hashBytes = new byte[48];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 32);

                //make the hash a string again
                return Convert.ToBase64String(hashBytes);
            }
        }

        // 128-bit salt
        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16]; 
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }


        public bool VerifyPassword(string password, string storedHash)
        {
            // Extract salt from stored hash
            byte[] hashBytes;
            try
            {
                hashBytes = Convert.FromBase64String(storedHash);
            }
            catch (FormatException)
            {
                throw new ArgumentException("The stored hash is not a valid Base64 string.");
            }

            if (hashBytes.Length < 48)
            {
                throw new ArgumentException("The stored hash is not long enough");
            }

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            //Rehash and compare
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                
                for (int i = 0; i < 32; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }
            }
            return true;         }
    }
}
