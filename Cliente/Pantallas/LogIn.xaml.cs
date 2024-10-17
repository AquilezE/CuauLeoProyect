using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Haley.Utils;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;
using Cliente.ServiceReference;

namespace Cliente.Pantallas
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : UserControl
    {

        private UsersManagerClient _servicio;

        public LogIn()
        {
            InitializeComponent();
            LangUtils.Register();
            ChangeCulture("es-ES");
            cbLanguage.SelectionChanged -= cbLanguage_SelectionChanged;
            cbLanguage.SelectedIndex = 0; 
            cbLanguage.SelectionChanged += cbLanguage_SelectionChanged;
            _servicio = new UsersManagerClient();
        }


        private void btPlayAsGuest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btLogIn_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string password = pbPassword.Password;


            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {

                try
                {

                    if (SetSessionUser(username, password))
                    {
                        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                        mainWindow.NavigateToView(new MainMenu());

                    }
                    else
                    {
                        lbErrLabel.Content = "Invalid username or password";
                    }
                }
                catch (Exception ex)
                {
                    lbErrLabel.Content = "An error occurred while trying to log in. Please try again later";
                }


            }
            else
            {
                lbErrLabel.Content = "Please enter a username and password";
            }
        }


        private bool SetSessionUser(string email, string password)
        {
            try
            {
                UserDto userDto = _servicio.LogIn(email, password);
                if (userDto == null)
                {
                    return false;
                }

                
                User currentUser = User.Instance;
                currentUser.ID = userDto.UserId;
                currentUser.Username = userDto.Username;
                currentUser.Email = userDto.Email;
                currentUser.ProfilePictureId = userDto.ProfilePictureId;

                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void btRegister_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new RegisterAccount());
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new RecoverPassword());
        }

        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = cbLanguage.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string culture = selectedItem.Content as string;
                if (culture != "cmbLanguage")
                {
                    ChangeCulture(culture);
                }
            }
        }

        private void ChangeCulture(string culture)
        {
            Console.WriteLine("Changing culture to: " + culture);
            try
            { 
            LangUtils.ChangeCulture(culture);
            }catch(CultureNotFoundException ex)
            {
                LangUtils.ChangeCulture("en");
                Console.WriteLine("Changing culture to: " + culture);
            }

        }

    }
}
