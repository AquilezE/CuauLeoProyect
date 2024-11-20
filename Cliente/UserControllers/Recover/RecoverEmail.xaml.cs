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
using Cliente.ServiceReference;
using Haley.Utils;


namespace Cliente.UserControllers.Recover
{
    public partial class RecoverEmail : UserControl
    {
        public event Action<string> EmailFilled;
        private UsersManagerClient _service;

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
           
            if (IsValidEmail(email))
            {
                if (_service.IsEmailTaken(email)) { 

                _service.SendTokenAsync(email);
                OnEmailFilled(email);
                
                }else
                {
                    lbErrEmailCode.Content = LangUtils.Translate("lblErrEmailNotFound");
                }
            }
            else
            {
                lbErrEmailCode.Content = LangUtils.Translate("lblErrEmailInvalid");
            }


        }


        private bool IsValidEmail(string email)
        {

            
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$";

                if (!Regex.IsMatch(email, pattern))
                {
                    return false;
                }

                var addr = new System.Net.Mail.MailAddress(email);
                string domain = addr.Host;

                return domain.IndexOf("..") == -1 && domain.All(c => Char.IsLetterOrDigit(c) || c == '-' || c == '.');
            }
            catch
            {

                return false;
            }
        }
    }
}
