using Cliente.ServiceReference;
using Haley.Utils;
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
                lbErrLobbyCode.Content = LangUtils.Translate("lblErrLobbyCodeNull");
            }
            else if (lobbyCodeText.Length > 10) 
            {
                lbErrLobbyCode.Content = LangUtils.Translate("lblErrLobbyCodeTooLong");
            }
            else if (!lobbyCodeText.All(char.IsDigit)) 
            {
                lbErrLobbyCode.Content = LangUtils.Translate("lblErrLobbyCodeNotNumeric");
            }
            else if (!int.TryParse(lobbyCodeText, out int lobbyId)) 
            {
                lbErrLobbyCode.Content = LangUtils.Translate("lblErrInvalidCodeFormat");
            }
            else if (lobbyId <= 4) 
            {
                lbErrLobbyCode.Content = LangUtils.Translate("lblErrLobbyCodeNotInRange");
            }
            else if (lobbyId > 1000000) 
            {
                lbErrLobbyCode.Content = LangUtils.Translate("lblErrLobbyCodeExceedRange");
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
                    lbErrLobbyCode.Content = LangUtils.Translate("lblErrLobbyCodeFullOrNotExists");
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
