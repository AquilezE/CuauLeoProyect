using Cliente.ServiceReference;
using Haley.Utils;
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
                    lbErrEmail.Content = LangUtils.Translate("lblErrEmailExists");
                    return;
                }

                if (isUsernameTaken)
                {
                    lbErrUsername.Content = LangUtils.Translate("lblErrUsernameExists");
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
                lbErrUsername.Content = LangUtils.Translate("lblErrUsernameInvalid");
            }
            else if (!IsValidUsername(username))
            {
                lbErrUsername.Content = LangUtils.Translate("lblErrUsernameInvalid");
            }
            else
            {
                lbErrUsername.Content = string.Empty;
            }
        }

        private bool IsValidUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9_]+$";
            return Regex.IsMatch(username, pattern);
        }

        private void tbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbEmail.Text) || !IsValidEmail(tbEmail.Text))
            {
                lbErrEmail.Content = LangUtils.Translate("lblErrEmailInvalid");
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
                lbErrPassword.Content = LangUtils.Translate("lblErrWeakPassword");
            }
            else if (pbPassword.Password.Length < 8)
            {
                lbErrPassword.Content = LangUtils.Translate("lblErrShortPassword");
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
                lbErrPasswordConfirmation.Content = LangUtils.Translate("lblErrDiferentPassword");
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
