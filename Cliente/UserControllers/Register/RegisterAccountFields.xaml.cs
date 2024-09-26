using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.Pantallas
{
    /// <summary>
    /// Interaction logic for RegisterAccountFields.xaml
    /// </summary>
    public partial class RegisterAccountFields : UserControl
    {
        public event EventHandler RegistrationFilled;

        public RegisterAccountFields()
        {
            InitializeComponent();
        }

        private void btRegister_Click(object sender, RoutedEventArgs e)
        {

            if (true)
            {
                OnRegistrationCompleted(EventArgs.Empty);
            }

        }

        protected virtual void OnRegistrationCompleted(EventArgs e)
        {
            RegistrationFilled?.Invoke(this, e);
        }

        private void tbUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(username))
            {
                lbErrUsername.Content = "Username cannot be empty.";
                isValid = false;
            }
            else if (!IsValidUsername(username))
            {
                lbErrUsername.Content = "Username can only contain letters, numbers, and underscores.";
                isValid = false;
            }
            else
            {
                lbErrUsername.Content = string.Empty;
            }

            if (isValid)
            {
                lbErrUsername.Content="shitvalid";
            }
        }

        private bool IsValidUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9_]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(username, pattern);
        }

        private void tbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbEmail.Text) || !IsValidEmail(tbEmail.Text))
            {
                lbErrEmail.Content = "Invalid email format.";
            }
            else
            {
                lbErrEmail.Content = string.Empty;
            }
        }

        private void pbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            string password = pbPassword.Password;
            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            if (!hasUpper || !hasLower || !hasDigit || !hasSpecialChar)
            {
                lbErrPassword.Content = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
            }
            else if (password.Length < 8)
            {
                lbErrPassword.Content = "Password must be at least 8 characters long.";
            }
            else
            {
                lbErrPassword.Content = string.Empty;
            }
        }

        private void pbConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pbConfirmPassword.Password != pbPassword.Password)
            {
                lbErrPasswordConfirmation.Content = "Passwords do not match.";
            }
            else
            {
                lbErrPasswordConfirmation.Content = string.Empty;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    
    }
}
