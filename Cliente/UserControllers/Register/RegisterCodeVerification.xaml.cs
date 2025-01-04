using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{

    public partial class RegisterCodeVerification : UserControl
    {

        public event EventHandler VerificationCompleted;
        private UsersManagerClient _service;
        private Validator _validator = new Validator();

        private string _email;
        private string _username;
        private string _password;

        private int _retryCounter = 0;
        private const int MaxRetries = 3;


        public RegisterCodeVerification(string email, string username, string password)
        {
            _service = new UsersManagerClient();
            InitializeComponent();
            _email = email;
            _username = username;
            _password = password;
        }

        private async void btRegister_Click(object sender, RoutedEventArgs e)
        {
            btRegister.IsEnabled = false;

            try
            {
                if (_validator.IsTokenValidFormat(tbVerificactionCode.Text))
                {
                    bool isCodeValid = await _service.VerifyTokenAsync(_email, tbVerificactionCode.Text);

                    if (isCodeValid)
                    {
                        bool isRegistered = await _service.RegisterUserAsync(_email, _username, _password);

                        if (isRegistered)
                        {
                            OnVerificationCompleted(EventArgs.Empty);
                        }
                        else
                        {
                            lbErrVerificactionCode.Content = LangUtils.Translate("lblErrUserOrEmailTaken");
                        }
                    }
                    else
                    {
                        HandleFailedVerification();
                    }
                }
                else
                {
                    lbErrVerificactionCode.Content = LangUtils.Translate("lblErrIncorrectCode");
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
            finally
            {
                btRegister.IsEnabled = true;
            }
        }


        private void HandleFailedVerification()
        {
            tbVerificactionCode.Clear();
            tbVerificactionCode.Focus();

            _retryCounter++;

            if (_retryCounter >= MaxRetries)
            {
                lbErrVerificactionCode.Content = LangUtils.Translate("lblErrManyCodeAttempts");
                btRegister.IsEnabled = false;
            }

            else
            {
                lbErrVerificactionCode.Content = LangUtils.Translate("lblErrIncorrectCode");
            }
        }

        protected virtual void OnVerificationCompleted(EventArgs e)
        {
            VerificationCompleted?.Invoke(this, e);
        }

        private async void ResendEmail_Click(object sender, RoutedEventArgs e)
        {
            lbResendEmail.IsEnabled = false;

            lbResendEmail.Content = LangUtils.Translate("lblSending");

            try
            {
                bool emailSent = await _service.SendTokenAsync(_email);

                lbResendEmail.Content = LangUtils.Translate(emailSent ? "lblCodeResent" : "lblErrFailedResendEmail");
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
            finally
            {
                const int cooldownPeriod = 30000;
                var timer = new System.Timers.Timer(cooldownPeriod);
                timer.Elapsed += (s, args) =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        lbResendEmail.IsEnabled = true;
                        lbResendEmail.Content = LangUtils.Translate("lblClickHereResend");
                    });
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
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