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

        public RegisterCodeVerification()
        {
            InitializeComponent();
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btRegister_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                OnVerificationCompleted(EventArgs.Empty);

            }
        }

        protected virtual void OnVerificationCompleted(EventArgs e)
        {
            VerificationCompleted?.Invoke(this, e);
        }

        private void ResendEmail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
