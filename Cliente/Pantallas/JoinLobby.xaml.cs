using Cliente.ServiceReference;
using Cliente.UserControllers;
using Cliente.Utils;
using Haley.Utils;
using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.Pantallas
{

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
            UserDTO userDto = GetCurrentUserDto();

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

            bool isLobbyOpen;
            bool isLobbyFull;

            try
            {
                isLobbyOpen = _lobbyCheckerClient.IsLobbyOpen(lobbyId);
                isLobbyFull = _lobbyCheckerClient.IsLobbyFull(lobbyId);
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                return;
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrConnectionTimeout"));
                return;
            }

            if (isLobbyOpen && !isLobbyFull)
            {
                try
                {
                    var lobbyWindow = new Lobby();
                    lobbyWindow.Servicio.JoinLobby(lobbyId, userDto);

                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(lobbyWindow);
                }
                catch (EndpointNotFoundException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    lbErrLobbyCode.Content = LangUtils.Translate("lblErrJoiningLobby");
                }
                catch (TimeoutException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    lbErrLobbyCode.Content = LangUtils.Translate("lblErrJoiningLobby");
                }
            }
            else
            {
                lbErrLobbyCode.Content = LangUtils.Translate("lblErrLobbyCodeFullOrNotExists");
            }
        }

        private UserDTO GetCurrentUserDto()
        {
            return new UserDTO
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
            if (User.Instance.ID > 0)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new MainMenu());
            }
            else if (User.Instance.ID < 0)
            {
                User.Instance = null;
                Social.Instance = null;
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new LogIn());
            }
        }

    }

}