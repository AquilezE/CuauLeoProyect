using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.Utils;
using System;
using System.Reflection.Emit;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Haley.Utils;

namespace Cliente.UserControllers.ChangePassword
{

    public partial class ChangePassword : UserControl
    {

        private ProfileManagerClient _service;
        private Validator _validator = new Validator();

        public ChangePassword()
        {
            _service = new ProfileManagerClient();
            InitializeComponent();
        }

        private void btChange_Click(object sender, RoutedEventArgs e)
        {
            string currentPassword = pbCurrentPassword.Password;
            string newPassword = pbNewPassword.Password;
            string confirmNewPassword = pbConfirmNewPassword.Password;

            string errorCurrent = _validator.ValidatePassword(currentPassword);
            string errorNewPassword = _validator.ValidatePassword(confirmNewPassword);
            string errorConfirmNewPassword = _validator.ValidateConfirmPassword(newPassword, confirmNewPassword);

            if (!string.IsNullOrEmpty(errorCurrent) ||
                !string.IsNullOrEmpty(errorNewPassword) ||
                !string.IsNullOrEmpty(errorConfirmNewPassword))
            {
                return;
            }

            try
            {

               int result = _service.ChangePassword(User.Instance.ID, pbCurrentPassword.Password, pbNewPassword.Password);

                switch (result)
                {
                    case 0:
                    {
                        var mainWindow = (MainWindow)Application.Current.MainWindow;
                        mainWindow.NavigateToView(new ChangePasswordSuccess(), 800, 700);
                        break;
                    }
                    case 1:
                        lbErrCuerrentPassword.Content = LangUtils.Translate("lblIncorrectPassword");
                        break;
                    case 2:
                        lbErrCuerrentPassword.Content = LangUtils.Translate("lblErrAccDoesntExist");
                        break;
                    case 3:
                        lbErrCuerrentPassword.Content = LangUtils.Translate("lblErrErrorChangingPassword");
                        break;

                }
            }
            catch (EndpointNotFoundException ex)
            {
                ResetServiceIfFaulted();
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (FaultException<BevososServerExceptions> ex)
            {
                ResetServiceIfFaulted();
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));
            }
            catch (TimeoutException ex)
            {
                ResetServiceIfFaulted();
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (CommunicationException ex)
            {
                ResetServiceIfFaulted();
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (Exception ex)
            {
                ResetServiceIfFaulted();
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrErrorChangingPassword"));
            }

        }

        private void pbCurrentPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            string password = pbCurrentPassword.Password;
            string error = _validator.ValidatePassword(password);
            lbErrCuerrentPassword.Content = error;
        }


        private void pbNewPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            string password = pbNewPassword.Password;
            string error = _validator.ValidatePassword(password);
            lbErrNewPassword.Content = error;
        }

        private void pbConfirmNewPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            string password = pbNewPassword.Password;
            string confirmPassword = pbConfirmNewPassword.Password;
            string error = _validator.ValidateConfirmPassword(password, confirmPassword);
            lbErrConfirmNewPassword.Content = error;
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Profile());
        }

        private void ResetServiceIfFaulted()
        {
            if (_service == null) return;
            ICommunicationObject commObj = _service;
            if (commObj.State != CommunicationState.Faulted) return;
            commObj.Abort();

            _service = new ProfileManagerClient();
        }
    }

}