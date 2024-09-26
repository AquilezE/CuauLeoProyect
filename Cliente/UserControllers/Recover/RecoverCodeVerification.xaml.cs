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

namespace Cliente.UserControllers.Recover
{
    /// <summary>
    /// Interaction logic for RecoverCodeVerification.xaml
    /// </summary>
    public partial class RecoverCodeVerification : UserControl
    {
        public event EventHandler VerificationCompleted;
        public RecoverCodeVerification()
        {
            InitializeComponent();
        }

        protected virtual void OnVerificationCompleted(EventArgs e)
        {
            VerificationCompleted?.Invoke(this, e);
        }

        private void btVerify_Click(object sender, RoutedEventArgs e)
        {
            OnVerificationCompleted(e);
        }

        private void ResendEmail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
