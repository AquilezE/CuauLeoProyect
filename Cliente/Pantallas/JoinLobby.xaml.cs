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
    /// Lógica de interacción para JoinLobby.xaml
    /// </summary>
    public partial class JoinLobby : UserControl
    {
        public JoinLobby()
        {
            InitializeComponent();
        }

        private void btJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            LobbyCheckerClient lobbyCheckerClient = new LobbyCheckerClient();

            Lobby lobbyWindow = new Lobby();

            UserDto userDto = new UserDto();
            userDto.UserId = User.Instance.ID;
            userDto.Username = User.Instance.Username;
            userDto.Email = User.Instance.Email;
            userDto.ProfilePictureId = User.Instance.ProfilePictureId;

            string lobbyCodeText = tbLobbyCode.Text.Trim();

            if (string.IsNullOrEmpty(lobbyCodeText))
            {
                lbErrLobbyCode.Content = "Lobby code cannot be empty.";
            }
            else if (lobbyCodeText.Length > 10) 
            {
                lbErrLobbyCode.Content = "Lobby code is too long.";
            }
            else if (!lobbyCodeText.All(char.IsDigit)) 
            {
                lbErrLobbyCode.Content = "Lobby code must be a numeric value.";
            }
            else if (!int.TryParse(lobbyCodeText, out int lobbyId)) 
            {
                lbErrLobbyCode.Content = "Invalid lobby code format.";
            }
            else if (lobbyId <= 4) 
            {
                lbErrLobbyCode.Content = "Lobby range invalid.";
            }
            else if (lobbyId > 1000000) 
            {
                lbErrLobbyCode.Content = "Lobby code exceeds the maximum allowed value.";
            }
            else
            {
                bool isLobbyOpen = lobbyCheckerClient.IsLobbyOpen(lobbyId);
                bool isLobbyFull = lobbyCheckerClient.IsLobbyFull(lobbyId);

                if (isLobbyOpen && !isLobbyFull)
                {
                    lobbyWindow._servicio.JoinLobby(lobbyId, userDto);

                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(lobbyWindow);
                }
                else
                {
                    lbErrLobbyCode.Content = "Lobby is full or doesn't exist.";
                }
            }
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }
    }
}
