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

namespace Cliente.Pantallas
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : UserControl
    {

        private LogInService.LogInClient _servicio;


        public LogIn()
        {
            LangUtils.Register();
            LangUtils.ChangeCulture("es-ES");
            InitializeComponent();
            InitializeService();
        }

        public void InitializeService()
        {
            _servicio = new LogInService.LogInClient();
            //_servicio.Open();
        }

        private void btPlayAsGuest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btLogIn_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string password = pbPassword.Password;

            // TODO: Implement login logic using the username and password variables

            // Example code to check if the username and password are not empty
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                LogInService.User user;

                try
                {
                     user = _servicio.TryLogIn(username, password);
                    if (user != null)
                    {

                        User currentUser = User.Instance;

                        currentUser.ID = user.ID;
                        currentUser.Username = user.Username;
                        currentUser.Email = user.Email; 

                        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                        mainWindow.NavigateToMensajeador(new MainMenu());

                    }
                    else
                    {
                        lbErrLabel.Content = "Invalid username or password";
                    }
                }
                catch (Exception ex)
                {
                    lbErrLabel.Content = "Error: " + ex.Message;
                }


            }
            else
            {
                lbErrLabel.Content = "Please enter a username and password";
            }
        }

        private void btRegister_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToMensajeador(new RegisterAccount());
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToMensajeador(new RecoverPassword());
        }

        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLanguage.SelectedItem != null)
            {             
                ChangeCulture(cbLanguage.SelectedItem as string);
            }
        }

        public static void ChangeCulture(string culture)
        {
            if (culture == "Spanish")
            {
                LangUtils.ChangeCulture("es-ES");

            }
            else if (culture == "Inglés")
            {
                LangUtils.ChangeCulture("en-EN");
            }
        }

    }
}
