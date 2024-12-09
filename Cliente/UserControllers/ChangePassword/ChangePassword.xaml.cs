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
    /// <summary>
    /// Lógica de interacción para ChangePassword.xaml
    /// </summary>

    public partial class ChangePassword : UserControl, IProfileManagerCallback
    {
        private ProfileManagerClient _service;
        private Validator _validator = new Validator();
        public ChangePassword()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            _service = new ProfileManagerClient(instanceContext);
            InitializeComponent();
        }

        private void btChange_Click(object sender, RoutedEventArgs e)
        {

            string currentPassword = pbCurrentPassword.Password;
            string newPassword = pbNewPassword.Password;
            string confirmNewPassword = pbConfirmNewPassword.Password;

            string errorCurrent = _validator.ValidatePassword(newPassword);
            string errorNewPassword= _validator.ValidatePassword(confirmNewPassword);
            string errorConfirmNewPassword = _validator.ValidateConfirmPassword(newPassword, confirmNewPassword);
            
            if(!string.IsNullOrEmpty(errorCurrent) || 
               !string.IsNullOrEmpty(errorNewPassword) || 
               !string.IsNullOrEmpty(errorConfirmNewPassword))
            {
                return;
            }

            try
            {
                _service.ChangePassword(User.Instance.ID, pbCurrentPassword.Password, pbNewPassword.Password);
            }catch(EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConnction"));
            }catch(FaultException<BevososServerExceptions> ex) {
                ExceptionManager.LogErrorException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));
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

        public void OnProfileUpdate(string username, int profilePictureId, string message)
        {
            throw new NotImplementedException();
        }

        public void OnPasswordChange(string error)
        {
            if (error != null)
            {
                lbErrCuerrentPassword.Content = error;
            }
            else
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new ChangePasswordSuccess(),800,700);
            }
        }


        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Profile(), 800, 700);
        }
    }
}
