using Cliente.Pantallas;
using Cliente.ServiceReference;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace Cliente.UserControllers.ChangePassword
{
    /// <summary>
    /// Lógica de interacción para ChangePassword.xaml
    /// </summary>

    public partial class ChangePassword : UserControl, IProfileManagerCallback
    {
        private ProfileManagerClient _service;
        public ChangePassword()
        {
            InstanceContext instanceContext = new InstanceContext(this);
            _service = new ProfileManagerClient(instanceContext);
            InitializeComponent();
        }

        private void btChange_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidPassword(pbNewPassword.Password) && pbConfirmNewPassword.Password == pbNewPassword.Password)
            {
                _service.ChangePassword(User.Instance.ID, pbCurrentPassword.Password, pbNewPassword.Password);    
            }
            else
            {

            }
        }

        private void pbCurrentPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pbCurrentPassword.Password))
            {
                lbErrCuerrentPassword.Content = "This field is obligatory";
            }
            else
            {
                lbErrCuerrentPassword.Content = string.Empty;
            }
        }

        private bool IsValidPassword(string password)
        {

            if (password.Contains(' '))
            {
                return false;
            }


            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasLower && hasDigit && hasSpecialChar && password.Length >= 8;
        }

        private void pbNewPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!IsValidPassword(pbNewPassword.Password))
            {
                lbErrNewPassword.Content = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
            }
            else if (pbNewPassword.Password.Length < 8)
            {
                lbErrNewPassword.Content = "Password must be at least 8 characters long.";
            }
            else
            {
                lbErrNewPassword.Content = string.Empty;
            }
        }

        private void pbConfirmNewPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pbConfirmNewPassword.Password != pbNewPassword.Password)
            {
                lbErrConfirmNewPassword.Content = "Passwords do not match.";
            }
            else
            {
                lbErrConfirmNewPassword.Content = string.Empty;
            }
        }
        public void OnPasswordChange(string error)
        {
            if (error != null)
            {
                lbErrCuerrentPassword.Content = error;
            }
            else
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new ChangePasswordSuccess(),800,700);
            }
        }

        public void OnProfileUpdate(string username, int profilePictureId, string error)
        {
            throw new NotImplementedException();
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Profile(), 800, 700);
        }
    }
}
