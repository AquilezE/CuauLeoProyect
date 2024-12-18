using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Cliente.UserControllers;

namespace Cliente.Pantallas
{

    public partial class RegisterAccountFields : UserControl
    {

        public event Action<string, string, string> RegistrationFilled;
        private readonly UsersManagerClient _service;
        private readonly Validator _validator = new Validator();


        public RegisterAccountFields()
        {
            _service = new UsersManagerClient();
            InitializeComponent();
        }

        private async void btRegister_Click(object sender, RoutedEventArgs e)
        {
            btRegister.IsEnabled = false;


            const int cooldownDuration = 2000;


            var timer = new System.Timers.Timer(cooldownDuration);
            timer.Elapsed += (s, args) =>
            {
                Dispatcher.Invoke(() => btRegister.IsEnabled = true);
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();

            string username = tbUsername.Text;
            string email = tbEmail.Text;
            string password = pbPassword.Password;
            string confirmPassword = pbConfirmPassword.Password;

            string usernameError = _validator.ValidateUsername(username);
            string emailError = _validator.ValidateEmail(email);
            string passwordError = _validator.ValidatePassword(password);
            string confirmPasswordError = _validator.ValidateConfirmPassword(password, confirmPassword);


            if (!string.IsNullOrEmpty(usernameError) ||
                !string.IsNullOrEmpty(emailError) ||
                !string.IsNullOrEmpty(passwordError) ||
                !string.IsNullOrEmpty(confirmPasswordError))
            {
                return;
            }

            try
            {
                bool isEmailTaken = await _service.IsEmailTakenAsync(email);
                bool isUsernameTaken = await _service.IsUsernameTakenAsync(username);

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

                OnRegistrationCompleted(username, password, email);
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
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
        }

        protected virtual void OnRegistrationCompleted(string username, string password, string email)
        {
            try
            {
                _service.SendTokenAsync(email);
                RegistrationFilled?.Invoke(username, password, email);
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
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
        }

        private void tbUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string error = _validator.ValidateUsername(username);
            lbErrUsername.Content = error;
        }

        private void tbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = tbEmail.Text;
            string error = _validator.ValidateEmail(email);
            lbErrEmail.Content = error;
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

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new LogIn());
        }

    }

}