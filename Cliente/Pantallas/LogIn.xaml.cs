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

        private void ChangeLanguage(string cultureCode)
        {
            // Cambiar la cultura del hilo actual
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

            try
            {
                // Crear un ResourceManager para cargar los recursos
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Cliente.Properties.Resources", typeof(LogIn).Assembly);

                // Obtener el recurso específico para la cultura
                var resourceSet = rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

                if (resourceSet != null)
                {
                    // Limpiar y agregar el nuevo diccionario de recursos
                    Application.Current.Resources.MergedDictionaries.Clear();
                    foreach (DictionaryEntry entry in resourceSet)
                    {
                        Application.Current.Resources[entry.Key] = entry.Value;
                    }

                    // Actualizar la interfaz de usuario
                    InitializeComponent();
                }
                else
                {
                    throw new FileNotFoundException($"Resource set not found for culture: {cultureCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el archivo de recursos: {ex.Message}");
            }
        }

        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                string selectedLanguage = selectedItem.Content.ToString();
                if (selectedLanguage == "Inglés")
                {
                    ChangeLanguage("en-US");
                }
                else if (selectedLanguage == "Spanish")
                {
                    ChangeLanguage("es-MX");
                }
            }
        }


    }
}
