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
    /// Interaction logic for RecoverEmail.xaml
    /// </summary>
    public partial class RecoverEmail : UserControl
    {
        public event EventHandler EmailFilled;

        public RecoverEmail()
        {
            InitializeComponent();
        }

        protected virtual void OnEmailFilled(EventArgs e)
        {
            EmailFilled?.Invoke(this, e);
        }

        private void btSendEmail_Click(object sender, RoutedEventArgs e)
        {
            OnEmailFilled(e);
        }
    }
}
