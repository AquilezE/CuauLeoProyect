using Cliente.Pantallas;
using Cliente.ServiceReference;
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

namespace Cliente.UserControllers
{
    /// <summary>
    /// Interaction logic for RegisterCodeVerification.xaml
    /// </summary>
    public partial class RegisterCodeVerification : UserControl
    {
        public event EventHandler VerificationCompleted;
        private UsersManagerClient _service;

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
                if (IsTokenValidFormat(tbVerificactionCode.Text))
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
                            lbErrVerificactionCode.Content = "Registration failed. The username or email may already be taken";
                        }
                    }
                    else
                    {
                        HandleFailedVerification();
                    }
                }
                else
                {
                    lbErrVerificactionCode.Content = "Invalid code format"; 
                }
            }
            catch(Exception ex)
            {
                lbErrVerificactionCode.Content = "An error ocurred while verifying the code, Please try again later";
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
                lbErrVerificactionCode.Content = "Too many failed attempts. Please request a new verification code.";
                btRegister.IsEnabled = false; 
            }

            else
            {
                lbErrVerificactionCode.Content = "Invalid code. Please try again.";
            }
        }
        protected virtual void OnVerificationCompleted(EventArgs e)
        {
            VerificationCompleted?.Invoke(this, e);
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
            lbResendEmail.IsEnabled = false;

            lbResendEmail.Content = "Sending...";

            try
            {
                bool emailSent = await _service.SendTokenAsync(_email);

                if (emailSent)
                {
                    lbErrVerificactionCode.Content = "Verification email has been resent.";
                }
                else
                {
                    lbErrVerificactionCode.Content = "Failed to resend the email. Please try again later.";
                }
            }
            catch (Exception ex)
            {
               
                lbErrVerificactionCode.Content = "An error occurred while resending the email.";
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
                        lbResendEmail.Content = "Click aqui para reenviar tu codigo";
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
