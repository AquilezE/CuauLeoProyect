using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
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
            UserDto userDto = new UserDto
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
            User.Instance = null;

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
