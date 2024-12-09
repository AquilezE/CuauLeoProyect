using System;
using Cliente.ServiceReference;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Haley.Utils;
using System.ServiceModel;
using Cliente.Utils;

namespace Cliente.UserControllers.Recover
{

    public partial class RecoverCodeVerification : UserControl
    {
        public event EventHandler VerificationCompleted;

        private UsersManagerClient _service = new UsersManagerClient();
        private string _email;  


        private int _retryCounter = 0;
        private const int MAX_RETRIES = 3;

        public RecoverCodeVerification(string email)
        {
            _email = email;
            InitializeComponent();
        }

        protected virtual void OnVerificationCompleted(EventArgs e)
        {
            VerificationCompleted?.Invoke(this, e);
        }

        private async void btVerify_Click(object sender, RoutedEventArgs e)
        {

            btVerify.IsEnabled = false;
            try
            {
                if (IsTokenValidFormat(tbVerificactionCode.Text)){
                    
                    bool isCodeValid = await _service.VerifyTokenAsync(_email, tbVerificactionCode.Text);
                    
                    if (isCodeValid)
                    {
                        OnVerificationCompleted(e);
                    }
                    else
                    {
                        HandleFailedVerification();
                    }
                }
                else
                {
                    lbErrVerificactionCode.Content=LangUtils.Translate("lblErrInvalidCodeFormat");
                }
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));

            }
            catch (FaultException<BevososServerExceptions> ex)
            {
                ExceptionManager.LogErrorException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));

            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrInvalidCode"));
            }
            finally
            {
                btVerify.IsEnabled = true;
            }

        }

        private void HandleFailedVerification()
        {
            tbVerificactionCode.Clear();
            tbVerificactionCode.Focus();

            _retryCounter++;

            if (_retryCounter >= MAX_RETRIES)
            {
                lbErrVerificactionCode.Content = LangUtils.Translate("lblErrCodeManyAttempts");
                btVerify.IsEnabled = false;
            }

            else
            {
                lbErrVerificactionCode.Content = LangUtils.Translate("lblErrInvalidCode");
            }
        }

        private bool IsTokenValidFormat(string code)
        {
            code = code.Trim();
            if (code.Length != 6)
            {
                return false;
            }
            if (!code.All(char.IsDigit))
            {
                return false;
            }
            return true;
        }

        private async void ResendEmail_Click(object sender, RoutedEventArgs e)
        {
            tbVerificactionCode.IsEnabled = false;

            tbVerificactionCode.Text = LangUtils.Translate("lblSending");

            try
            {
                bool emailSent = await _service.SendTokenAsync(_email);

                if (emailSent)
                {
                    lbErrVerificactionCode.Content = LangUtils.Translate("lblCodeResent");
                }
                else
                {
                    lbErrVerificactionCode.Content = LangUtils.Translate("lblErrFailedResendEmail");
                }
            }catch(EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));

            }
            catch (FaultException <BevososServerExceptions> ex)
            {
                ExceptionManager.LogErrorException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));

            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                NotificationDialog notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrFailedResendEmail"));
            }
            finally
            {

                const int cooldownPeriod = 30000;
                var timer = new System.Timers.Timer(cooldownPeriod);
                timer.Elapsed += (s, args) =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        tbVerificactionCode.IsEnabled = true;
                        tbVerificactionCode.Text = LangUtils.Translate("lblClickHereResend");
                    });
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }
    }
}
