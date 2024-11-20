using Cliente.ServiceReference;
using Haley.Utils;
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

namespace Cliente.UserControllers.Recover
{
    /// <summary>
    /// Interaction logic for RecoverNewPassword.xaml
    /// </summary>
    /// 

    public partial class RecoverNewPassword : UserControl
    {
        public event EventHandler PasswordChanged;
        private UsersManagerClient _service;
        private string _email;

        public RecoverNewPassword(string email)
        {
            _service = new UsersManagerClient();
            InitializeComponent();
            _email = email;
        }

        protected virtual void OnPasswordChanged(EventArgs e)
        {
            PasswordChanged?.Invoke(this, e);
        }

        private async void btChangePassword_Click(object sender, RoutedEventArgs e)
        {
            btChangePassword.IsEnabled = false;

            if(IsValidPassword(pbPassword.Password) && pbConfirmPassword.Password==pbPassword.Password)
            {
                bool passwordChanged = await _service.RecoverPasswordAsync(_email, pbPassword.Password);

                
                if(passwordChanged)
                {
                    OnPasswordChanged(e);
                }
                else
                {
                    lbErrPassword.Content = LangUtils.Translate("lblErrErrorChangingPassword");
                    btChangePassword.IsEnabled = true;
                }
            }
            else
            {
                lbErrPassword.Content = LangUtils.Translate("lblErrInvalidPassword");
                btChangePassword.IsEnabled = true;
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


    }
}
