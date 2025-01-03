using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;


namespace Cliente.UserControllers.Recover
{

    public partial class RecoverEmail : UserControl
    {

        public event Action<string> EmailFilled;
        private UsersManagerClient _service;
        private Validator _validator = new Validator();


        public RecoverEmail()
        {
            _service = new UsersManagerClient();
            InitializeComponent();
        }

        protected virtual void OnEmailFilled(string email)
        {
            EmailFilled?.Invoke(email);
        }

        private void btSendEmail_Click(object sender, RoutedEventArgs e)
        {
            string email = tbEmail.Text.Trim();

            string error = _validator.ValidateEmail(email);

            lbErrEmailCode.Content = error;

            if (error != string.Empty)
            {
                return;
            }

            try
            {
                if (_service.IsEmailTaken(email))
                {
                    _service.SendTokenAsync(email);
                    OnEmailFilled(email);
                }
                else
                {
                    lbErrEmailCode.Content = LangUtils.Translate("lblErrEmailNotFound");
                }
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                ResetServiceIfFaulted();
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
                ResetServiceIfFaulted();
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                ResetServiceIfFaulted();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                ResetServiceIfFaulted();
            }
        }


        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new LogIn());
        }

        private void ResetServiceIfFaulted()
        {
            if (_service == null) return;
            ICommunicationObject commObj = _service;
            if (commObj.State != CommunicationState.Faulted) return;
            commObj.Abort();

            _service = new UsersManagerClient();
        }

    }

}