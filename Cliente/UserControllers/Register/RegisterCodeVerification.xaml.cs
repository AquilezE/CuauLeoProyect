using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.UserControllers
{
    /// <summary>
    /// Interaction logic for RegisterCodeVerification.xaml
    /// </summary>
    public partial class RegisterCodeVerification : UserControl
    {
        public event EventHandler VerificationCompleted;
        private UsersManagerClient _service;
        private Validator _validator = new Validator();

        private string _email;
        private string _username;
        private string _password;

        private int _retryCounter = 0;
        private const int MAX_RETRIES = 3;
        

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
            catch(Exception ex)
            {
                lbErrVerificactionCode.Content = LangUtils.Translate("lblErrNoConection");
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

            if (_retryCounter >= MAX_RETRIES)
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

            lbResendEmail.Content = LangUtils.Translate("lblResendingCode");

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
            }
            catch (Exception ex)
            {
               
                lbErrVerificactionCode.Content = LangUtils.Translate("lblErrNoConection");
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
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new LogIn());
        }
    }
}
