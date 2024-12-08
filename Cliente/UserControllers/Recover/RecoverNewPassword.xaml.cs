using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

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
        private Validator _validator = new Validator();

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

            string password = pbPassword.Password;
            string confirmPassword = pbConfirmPassword.Password;

            string error = _validator.ValidatePassword(password);
            lbErrPassword.Content = error;

            string errorConfirmation = _validator.ValidateConfirmPassword(password,confirmPassword);
            lbErrPassword.Content = errorConfirmation;

            if (error != string.Empty || errorConfirmation != string.Empty)
            {
                btChangePassword.IsEnabled = true;
                return;
            }

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

        private void pbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            string password = pbPassword.Password;

            string error = _validator.ValidatePassword(password);
            lbErrPassword.Content = error;

        }

        private void pbConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            string password = pbPassword.Password;
            string confirmPassword = pbConfirmPassword.Password;

            string error = _validator.ValidateConfirmPassword(password, confirmPassword);
            lbErrPasswordConfirmation.Content = error;

        }

    }
}
