using Haley.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cliente.Utils
{

    public class Validator
    {

        public string ValidateEmail(string email)
        {
            if (email == null || string.IsNullOrWhiteSpace(email))
            {
                return LangUtils.Translate("lblErrEmailEmpty");
            }

            if (email.Length > 255)
            {
                return LangUtils.Translate("lblErrEmailTooLong");
            }


            if (email.Contains(' '))
            {
                return LangUtils.Translate("lblErrEmailContainsWhiteSpace");
            }

            if (!IsValidEmail(email))
            {
                return LangUtils.Translate("lblErrEmailInvalid");
            }

            return string.Empty;
        }

        public string ValidateUsername(string username)
        {
            if (username == null)
            {
                return LangUtils.Translate("lblErrUsernameEmpty");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return LangUtils.Translate("lblErrUsernameEmpty");
            }

            if (username.Length > 50)
            {
                return LangUtils.Translate("lblErrUsernameTooLong");
            }

            if (!IsValidUsername(username))
            {
                return LangUtils.Translate("lblErrUsernameInvalid");
            }


            return string.Empty;
        }

        public string ValidatePassword(string password)
        {
            if (password == null || string.IsNullOrWhiteSpace(password))
            {
                return LangUtils.Translate("lblErrPasswordEmpty");
            }

            if (password.Length < 12)
            {
                return LangUtils.Translate("lblErrShortPassword");
            }
            
            if (password.Length > 255)
            {
                return LangUtils.Translate("lblErrPasswordTooLong");
            }

            if (password.Contains(' '))
            {
                return LangUtils.Translate("lblErrPasswordContainsWhiteSpace");
            }

            if (!IsValidPassword(password))
            {
                return LangUtils.Translate("lblErrWeakPassword");
            }

            return string.Empty;
        }

        public bool IsTokenValidFormat(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return false;
            }

            code = code.Trim();
            return code.Length == 6 && code.All(char.IsDigit);
        }

        public bool IsValidPassword(string password)
        {
            if (password.Contains(' '))
            {
                return false;
            }

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasLower && hasDigit && hasSpecialChar && password.Length >= 8;
        }

        public bool IsValidUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9_]+$";
            return Regex.IsMatch(username, pattern);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$";

                if (!Regex.IsMatch(email, pattern))
                {
                    return false;
                }

                var addr = new System.Net.Mail.MailAddress(email);
                string domain = addr.Host;

                return domain.IndexOf("..") == -1 && domain.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '.');
            }
            catch
            {
                return false;
            }
        }

        public string ValidateConfirmPassword(string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                return LangUtils.Translate("lblErrPasswordEmpty");
            }

            if (confirmPassword != password)
            {
                return LangUtils.Translate("lblErrDiferentPassword");
            }

            return string.Empty;
        }

    }

}