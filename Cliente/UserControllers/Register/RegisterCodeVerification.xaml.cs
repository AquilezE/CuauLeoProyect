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

        public RegisterCodeVerification(string email, string username, string password)
        {
            _service = new UsersManagerClient();
            InitializeComponent();
            _email = email;
            _username = username;
            _password = password;
        }

        private void btRegister_Click(object sender, RoutedEventArgs e)
        {

            if (IsTokenValidFormat(tbVerificactionCode.Text))
            {

                bool isCodeValid = _service.VerifyToken(_email, tbVerificactionCode.Text);

                if (isCodeValid)
                {
                    if (_service.RegisterUser(_email, _username, _password)) 
                    { 
                    OnVerificationCompleted(EventArgs.Empty);
                    }else lbErrVerificactionCode.Content = "Error registering user";
                }
                else
                {

                    tbVerificactionCode.Clear();
                    tbVerificactionCode.Focus();
                    lbErrVerificactionCode.Content = "Invalid code YOU NEED TO INTERNATIONALIZE THIS PAL";
                }
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


        private void ResendEmail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
