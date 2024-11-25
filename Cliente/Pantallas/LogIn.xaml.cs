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
using static MaterialDesignThemes.Wpf.Theme;
using Cliente.GameUserControllers;

namespace Cliente.Pantallas
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : UserControl
    {

        private UsersManagerClient _servicio;
        private bool _isLoaded;

        public LogIn()
        {
            InitializeComponent();
            _servicio = new UsersManagerClient();
            LangUtils.Register();
            ChangeCulture("en");
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
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
                        lbErrLabel.Content = LangUtils.Translate("lblErrWrongLogin");
                    }
                }
                catch (Exception ex)
                {
                    lbErrLabel.Content = LangUtils.Translate("lblErrNoConection");
                }


            }
            else
            {
                lbErrLabel.Content = LangUtils.Translate("lblErrNullLogin");
            }
        }


        private bool SetSessionUser(string email, string password)
        {
            try
            {
                Social social = Social.Instance;

                if (Social.Instance.socialManagerClient.IsConnected(email))
                {
                    Social.Instance = null;
                    return false;
                }

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
                
                
                Social.Instance.socialManagerClient.Connect(currentUser.ID);
                RefreshSocialData(social);

                return true;
            }
            catch (Exception)
            {

                Social.Instance = null;
                Social.Instance = Social.Instance;

                if (_servicio != null)
                {
                    _servicio.Abort();
                    _servicio = new UsersManagerClient();
                }

                throw;
            }

        }

        private void RefreshSocialData(Social social)
        {
            social.FriendList.Clear();
            social.GetFriends();

            social.FriendRequests.Clear();
            social.GetFriendRequests();

            social.BlockedUsersList.Clear();
            social.GetBlockedUsers();
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
            if (!_isLoaded)
            {
                return;
            }

            ComboBoxItem selectedItem = cbLanguage.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string culture = selectedItem.Content as string;
                ChangeCulture(culture);
            }
        }

        private void ChangeCulture(string culture)
        {
            try
            {
                switch (culture)
                {
                    case "Spanish":
                        Console.WriteLine("Changing culture to: " + culture);
                        LangUtils.ChangeCulture("es");
                        lbErrLabel.Content = string.Empty;
                        break;
                    case "Inglés":
                        Console.WriteLine("Changing culture to: " + culture);
                        LangUtils.ChangeCulture("en");
                        lbErrLabel.Content = string.Empty;
                        break;
                    default:
                        LangUtils.ChangeCulture("en");
                        lbErrLabel.Content = string.Empty;
                        break;
                }
            }
            catch (CultureNotFoundException ex)
            {
                Console.WriteLine("Error changing culture: " + ex.Message);
            }
        }

    }
}
