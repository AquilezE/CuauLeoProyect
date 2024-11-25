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

        private readonly LobbyCheckerClient _lobbyCheckerClient;

        public JoinLobby()
        {
            InitializeComponent();
            _lobbyCheckerClient = new LobbyCheckerClient();
            lbErrLobbyCode.Content = "";
        }

        private void btJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            var userDto = GetCurrentUserDto();

            string lobbyCodeInput = tbLobbyCode.Text.Trim();

            string validationError = ValidateLobbyCode(lobbyCodeInput);
            if (validationError != null)
            {
                lbErrLobbyCode.Content = LangUtils.Translate(validationError);
                return;
            }

            if (!int.TryParse(lobbyCodeInput, out int lobbyId))
            {
                lbErrLobbyCode.Content = LangUtils.Translate("lblErrInvalidCodeFormat");
                return;
            }

            // Check lobby status
            bool isLobbyOpen;
            bool isLobbyFull;

            try
            {
                isLobbyOpen = WcfCallHelper.Execute(() => _lobbyCheckerClient.IsLobbyOpen(lobbyId));
                isLobbyFull = WcfCallHelper.Execute(() => _lobbyCheckerClient.IsLobbyFull(lobbyId));
            }
            catch (Exception)
            {
                lbErrLobbyCode.Content = LangUtils.Translate("lblErrNoConnection");
                return;
            }

            if (isLobbyOpen && !isLobbyFull)
            {
                try
                {
                    var lobbyWindow = new Lobby();
                    lobbyWindow._servicio.JoinLobby(lobbyId, userDto);

                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(lobbyWindow);
                }
                catch (Exception)
                {
                    lbErrLobbyCode.Content = LangUtils.Translate("lblErrJoiningLobby");
                }
            }
            else
            {
                lbErrLobbyCode.Content = LangUtils.Translate("lblErrLobbyCodeFullOrNotExists");
            }
        }

        private UserDto GetCurrentUserDto()
        {
            return new UserDto
            {
                UserId = User.Instance.ID,
                Username = User.Instance.Username,
                Email = User.Instance.Email,
                ProfilePictureId = User.Instance.ProfilePictureId
            };
        }

        private string ValidateLobbyCode(string lobbyCode)
        {
            if (string.IsNullOrEmpty(lobbyCode))
            {
                return "lblErrLobbyCodeNull";
            }

            if (lobbyCode.Length > 10)
            {
                return "lblErrLobbyCodeTooLong";
            }

            if (!lobbyCode.All(char.IsDigit))
            {
                return "lblErrLobbyCodeNotNumeric";
            }

            if (int.TryParse(lobbyCode, out int lobbyId))
            {
                if (lobbyId <= 4)
                {
                    return "lblErrLobbyCodeNotInRange";
                }

                if (lobbyId > 1000000)
                {
                    return "lblErrLobbyCodeExceedRange";
                }
            }
            else
            {
                return "lblErrInvalidCodeFormat";
            }

            return null;
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
        }
    }
}

