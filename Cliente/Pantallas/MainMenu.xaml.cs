using Cliente.ServiceReference;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.Pantallas
{
    /// <summary>
    /// Lógica de interacción para MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btCreateLobby_Click(object sender, RoutedEventArgs e)
        {
            Lobby lobbyWindow = new Lobby();


            //How convenient is it to add a cast to the User class?
            UserDTO userDto = new UserDTO
            {
                UserId = User.Instance.ID,
                Username = User.Instance.Username,
                Email = User.Instance.Email,
                ProfilePictureId = User.Instance.ProfilePictureId
            };

            lobbyWindow._servicio.NewLobbyCreated(userDto);

            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(lobbyWindow);
        }

        private void btJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new JoinLobby(), 650, 800);
        }

        private void btFriends_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Friends());
        }

        private void btStats_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new Stats(), 650, 800);
        }

        private void btLogOut_Click(object sender, RoutedEventArgs e)
        {
            Social.Instance.Logout();
            User.Instance = null;
            Social.Instance = null;

            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new LogIn());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.NavigateToView(new Profile());

            Console.WriteLine("Profile");
            Console.WriteLine(User.Instance.Username);
            Console.WriteLine(User.Instance.ID);
            Console.WriteLine(User.Instance.Email);
            Console.WriteLine(User.Instance.ProfilePictureId);
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
