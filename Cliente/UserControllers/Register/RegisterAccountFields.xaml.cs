using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public event Action<string, string, string> RegistrationFilled;
        private UsersManagerClient _service;

        public RegisterAccountFields()
        {
            _service = new UsersManagerClient();
            InitializeComponent();
        }

        private async void btRegister_Click(object sender, RoutedEventArgs e)
        {

            btRegister.IsEnabled = false;


            int cooldownDuration = 2000;


            var timer = new System.Timers.Timer(cooldownDuration);
            timer.Elapsed += (s, args) =>
            {
                Dispatcher.Invoke(() => btRegister.IsEnabled = true);
                timer.Stop(); 
                timer.Dispose(); 
            };
            timer.Start();


            if (IsValidUsername(tbUsername.Text) &&
                IsValidEmail(tbEmail.Text) &&
                IsValidPassword(pbPassword.Password) &&
                pbConfirmPassword.Password == pbPassword.Password)
            {

                bool isEmailTaken = await _service.IsEmailTakenAsync(tbEmail.Text);
                bool isUsernameTaken = await _service.IsUsernameTakenAsync(tbUsername.Text);

                if (isEmailTaken)
                {
                    lbErrEmail.Content = "Email is already taken.";
                    return;
                }

                if (isUsernameTaken)
                {
                    lbErrUsername.Content = "Username is already taken.";
                    return;
                }

                OnRegistrationCompleted(tbUsername.Text, pbPassword.Password, tbEmail.Text);
            }
            else
            {

            }
        }

        protected virtual void OnRegistrationCompleted(string username, string password, string email)
        {
            _service.SendTokenAsync(email);
            RegistrationFilled?.Invoke(username, password, email);
        }

        private void tbUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                lbErrUsername.Content = "Username cannot be empty.";
            }
            else if (!IsValidUsername(username))
            {
                lbErrUsername.Content = "Username can only contain letters, numbers, and underscores.";
            }
            else
            {
                lbErrUsername.Content = string.Empty;
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

        private bool IsValidEmail(string email)
        {
            try
            {
                string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$";

                if (!Regex.IsMatch(email, pattern))
                {
                    return false;
                }

                var addr = new System.Net.Mail.MailAddress(email);
                string domain = addr.Host;

                return domain.IndexOf("..") == -1 && domain.All(c => Char.IsLetterOrDigit(c) || c == '-' || c == '.');
            }
            catch
            {

            return false; 
            }
        }

        private void pbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!IsValidPassword(pbPassword.Password))
            {
                lbErrPassword.Content = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
            }
            else if (pbPassword.Password.Length < 8)
            {
                lbErrPassword.Content = "Password must be at least 8 characters long.";
            }
            else
            {
                lbErrPassword.Content = string.Empty;
            }
        }

        private bool IsValidPassword(string password)
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

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new LogIn());
        }
    }

}
