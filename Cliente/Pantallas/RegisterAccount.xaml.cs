using Cliente.UserControllers;
using System;
using System.Windows.Controls;

namespace Cliente.Pantallas
{
    /// <summary>
    /// Interaction logic for RegisterAccount.xaml
    /// </summary>
    /// 

    //this will implement the IRegisterAccountService interface
    public partial class RegisterAccount : UserControl
    {

        private string _username;
        private string _password;
        private string _email;
        public RegisterAccount()
        {
            InitializeComponent();
            var registerAccountFields = new RegisterAccountFields();
            registerAccountFields.RegistrationFilled += OnRegistrationFilled;
            RegisterAccountContentControl.Content = registerAccountFields;
        }

        private void OnRegistrationFilled(string username, string password, string email)
        {
            _username = username;
            _password = password;
            _email = email;
            
            var registerCodeVerification = new RegisterCodeVerification(_email, _username, _password);
            registerCodeVerification.VerificationCompleted += OnVerificationCompleted;
            RegisterAccountContentControl.Content = registerCodeVerification;
        }

        private void OnVerificationCompleted(object sender, EventArgs e)
        {
            RegisterAccountContentControl.Content = new RegisterSuccess();
        }
    }
}
