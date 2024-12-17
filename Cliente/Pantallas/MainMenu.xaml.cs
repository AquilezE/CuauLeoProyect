using Cliente.ServiceReference;
using Cliente.UserControllers;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.Pantallas
{

    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btCreateLobby_Click(object sender, RoutedEventArgs e)
        {
            var lobbyWindow = new Lobby();


            var userDto = new UserDTO
            {
                UserId = User.Instance.ID,
                Username = User.Instance.Username,
                Email = User.Instance.Email,
                ProfilePictureId = User.Instance.ProfilePictureId
            };

            try
            {
                lobbyWindow._servicio.NewLobbyCreated(userDto);

                var main = (MainWindow)Application.Current.MainWindow;
                main.NavigateToView(lobbyWindow);
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
        }

        private void btJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new JoinLobby(), 650, 800);
        }

        private void btFriends_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }

        private void btStats_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Stats(), 650, 800);
        }

        private void btLogOut_Click(object sender, RoutedEventArgs e)
        {
            Social.Instance.Logout();
            User.Instance = null;
            Social.Instance = null;

            var main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new LogIn());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            var main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new Profile());

            Console.WriteLine("Profile");
            Console.WriteLine(User.Instance.Username);
            Console.WriteLine(User.Instance.ID);
            Console.WriteLine(User.Instance.Email);
            Console.WriteLine(User.Instance.ProfilePictureId);
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            var main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new Options());
        }
    }
}