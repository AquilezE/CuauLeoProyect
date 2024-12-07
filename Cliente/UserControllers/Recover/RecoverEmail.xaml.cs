using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.Utils;
using Haley.Utils;


namespace Cliente.UserControllers.Recover
{
    public partial class RecoverEmail : UserControl
    {
        public event Action<string> EmailFilled;
        private UsersManagerClient _service;
        private Validator _validator = new Validator();



        public RecoverEmail()
        {
            _service = new UsersManagerClient();
            InitializeComponent();
        }

        protected virtual void OnEmailFilled(string email)
        {
            EmailFilled?.Invoke(email);
        }

        private void btSendEmail_Click(object sender, RoutedEventArgs e)
        {

            string email = tbEmail.Text.Trim();
           
            string error = _validator.ValidateEmail(email);

            lbErrEmailCode.Content = error;

            if (error != string.Empty)
            {
                return;
            }

   
            if (_service.IsEmailTaken(email)) { 

                _service.SendTokenAsync(email);
                OnEmailFilled(email);
                
            }else
            {
                lbErrEmailCode.Content = LangUtils.Translate("lblErrEmailNotFound");
            }
  

        }



        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new LogIn());
        }
    }
}
