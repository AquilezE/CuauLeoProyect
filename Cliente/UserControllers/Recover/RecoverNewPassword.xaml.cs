using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers.Recover
{

    public partial class RecoverNewPassword : UserControl
    {
        public event EventHandler PasswordChanged;
        private UsersManagerClient _service;
        private string _email;
        private Validator _validator = new Validator();

        public RecoverNewPassword(string email)
        {
            InitializeComponent();
            _service = new UsersManagerClient();
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

            string errorConfirmation = _validator.ValidateConfirmPassword(password, confirmPassword);
            lbErrPassword.Content = errorConfirmation;

            if (error != string.Empty || errorConfirmation != string.Empty)
            {
                btChangePassword.IsEnabled = true;
                return;
            }

            try
            {
                bool passwordChanged = await _service.RecoverPasswordAsync(_email, pbPassword.Password);


                if (passwordChanged)
                    OnPasswordChanged(e);
                else
                {
                    lbErrPassword.Content = LangUtils.Translate("lblErrErrorChangingPassword");
                    btChangePassword.IsEnabled = true;
                }
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (FaultException<BevososServerExceptions> ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrErrorChangingPassword"));
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