using System;
using System.Globalization;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Haley.Utils;
using Cliente.ServiceReference;
using Cliente.UserControllers;
using Cliente.Utils;

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
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
        }

        private void btPlayAsGuest_Click(object sender, RoutedEventArgs e)
        {
            if (SetSessionGuestUser())
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new JoinLobby(), 650, 800);
            }
        }

        private void btLogIn_Click(object sender, RoutedEventArgs e)
        {
            string email = tbUsername.Text;
            string password = pbPassword.Password;


            if (email.Length < 256 && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) &&
                password.Length < 256)
            {
                if (SetSessionUser(email, password))
                {
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(new MainMenu());
                }
                else
                    lbErrLabel.Content = LangUtils.Translate("lblErrWrongLogin");
            }
            else
                lbErrLabel.Content = LangUtils.Translate("lblErrNullLogin");
        }

        private bool SetSessionGuestUser()
        {
            try
            {
                UserDTO userDto = _servicio.GetGuestUser();
                if (userDto == null) return false;

                var currentUser = User.Instance;
                currentUser.ID = userDto.UserId;
                currentUser.Username = userDto.Username;
                currentUser.Email = userDto.Email;
                currentUser.ProfilePictureId = userDto.ProfilePictureId;
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));

                ResetClientGuest();
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));

                ResetClientGuest();
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));

                ResetClientGuest();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrFatal"));

                ResetClientGuest();
            }

            return true;
        }

        private bool SetSessionUser(string email, string password)
        {
            try
            {
                var social = Social.Instance;


                if (Social.Instance.socialManagerClient.IsConnected(email))
                {
                    Social.Instance = null;
                    return false;
                }

                UserDTO userDto = _servicio.LogIn(email, password);
                if (userDto == null) return false;


                var currentUser = User.Instance;
                currentUser.ID = userDto.UserId;
                currentUser.Username = userDto.Username;
                currentUser.Email = userDto.Email;
                currentUser.ProfilePictureId = userDto.ProfilePictureId;


                Social.Instance.socialManagerClient.Connect(currentUser.ID);
                RefreshSocialData(social);

                return true;
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                ResetSocial();
                return false;
            }
            catch (FaultException<BevososServerExceptions> ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoDataBase"));
                ResetSocial();
                return false;
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
                ResetSocial();
                return false;
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                ResetSocial();
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                ResetSocial();
                return false;
            }
        }

        private void ResetSocial()
        {
            Social.Instance = null;
            Social.Instance = Social.Instance;

            if (_servicio != null)
            {
                _servicio.Abort();
                _servicio = new UsersManagerClient();
            }
        }

        private void ResetClientGuest()
        {
            if (_servicio != null)
            {
                _servicio.Abort();
                _servicio = new UsersManagerClient();
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
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new RegisterAccount());
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new RecoverPassword());
        }

        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isLoaded) return;

            var selectedItem = cbLanguage.SelectedItem as ComboBoxItem;
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
                        LangUtils.ChangeCulture("es");
                        lbErrLabel.Content = string.Empty;
                        break;
                    case "Inglés":
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