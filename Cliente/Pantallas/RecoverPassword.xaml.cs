using Cliente.UserControllers.Recover;
using System;
using System.Windows.Controls;

namespace Cliente.Pantallas
{
    /// <summary>
    /// Interaction logic for RecoverPassword.xaml
    /// </summary>
    public partial class RecoverPassword : UserControl
    {

        private string _email;

        public RecoverPassword()
        {
            InitializeComponent();
            var recoverEmail = new RecoverEmail();
            recoverEmail.EmailFilled += OnEmailSent;
            RecoverPasswordContentControl.Content = recoverEmail;
        }

        private void OnEmailSent(string email)
        {
            _email = email;
            var registerCodeVerification = new RecoverCodeVerification(email);
            registerCodeVerification.VerificationCompleted += OnVerificationCompleted;
            RecoverPasswordContentControl.Content = registerCodeVerification;
        }

        private void OnVerificationCompleted(object sender, EventArgs e)
        {
            var recoverNewPassword = new RecoverNewPassword(_email);
            recoverNewPassword.PasswordChanged += OnPasswordChanged;
            RecoverPasswordContentControl.Content = recoverNewPassword;
            
        }

        private void OnPasswordChanged(object sender, EventArgs e)
        {
            RecoverPasswordContentControl.Content = new RecoverSuccesful();
        }



    }
}
