using Cliente.ServiceReference;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Cliente.UserControllers;
using System.Windows.Media;
using Haley.Utils;
using System.ComponentModel;
using Cliente.GameUserControllers;
using Cliente.Utils;

namespace Cliente.Pantallas
{

    public partial class Lobby : UserControl, ILobbyManagerCallback, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Message> _messages;
        private ObservableCollection<UserLobby> _users;


        public LobbyManagerClient _servicio;
        private int _lobbyId;

        private int _currentLeaderId;

        public int CurrentLeaderId
        {
            get => _currentLeaderId;
            set
            {
                if (_currentLeaderId != value)
                {
                    _currentLeaderId = value;
                    OnPropertyChanged(nameof(CurrentLeaderId));
                    OnPropertyChanged(nameof(IsLeader));
                }
            }
        }

        public bool IsLeader => _currentLeaderId == User.Instance.ID;
        public int CurrentUserId => User.Instance.ID;


        public Lobby()
        {
            InitializeComponent();
            DataContext = this;

            var instanceContext = new InstanceContext(this);
            _servicio = new LobbyManagerClient(instanceContext);
            _messages = new ObservableCollection<Message>();
            _users = new ObservableCollection<UserLobby>();
            MessagesListBox.ItemsSource = _messages;
            UsersListBox.ItemsSource = _users;
            lbUserName.Content = User.Instance.Username;
        }

        private void btLeaveLobby_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _servicio.LeaveLobby(_lobbyId, User.Instance.ID);
                LeaveLobby();
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                LeaveLobby();
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
                LeaveLobby();
            }
        }

        private void btSendMessage_Click(object sender, RoutedEventArgs e)
        {
            string message = tbMessage.Text.Trim();

            if (!IsValidMessage(message))
            {
                return;
            }

            _servicio.SendMessage(_lobbyId, User.Instance.ID, message);
            tbMessage.Text = string.Empty;
            lbMessageError.Content = string.Empty;
        }

        private bool IsValidMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                lbMessageError.Content = LangUtils.Translate("lblErrEmptyMessage");
                return false;
            }

            if (message.Length > 200)
            {
                lbMessageError.Content = LangUtils.Translate("lblErrMessageTooLong");
                return false;
            }

            return true;
        }

        private void btStartGame_Click(object sender, RoutedEventArgs e)
        {
            if (IsLeader)
            {
                if (_users.Count < 2)
                {
                    var notification = new NotificationDialog();
                    notification.ShowErrorNotification(LangUtils.Translate("lblErrNotEnoughPlayers"));
                    return;
                }

                if (_users.Any(u => u.IsReady == false))
                {
                    var notification = new NotificationDialog();
                    notification.ShowErrorNotification(LangUtils.Translate("lblErrNotAllReady"));
                    return;
                }

                _servicio.StartGame(_lobbyId);
            }
        }


        private void UserLobby_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is UserControllers.UserLobby userLobbyControl)
            {
                userLobbyControl.KickRequested += OnKickRequested;
            }
        }

        private void OnKickRequested(object sender, UserLobby userToKick)
        {
            if (userToKick != null)
            {
                var kickDialog = new KickUserDialog(userToKick.Username)
                {
                    Owner = Window.GetWindow(this)
                };

                bool? result = kickDialog.ShowDialog();

                if (result == true)
                {
                    string reason = kickDialog.KickReason;

                    _users.Remove(userToKick);

                    _servicio.KickUser(_lobbyId, User.Instance.ID, userToKick.ID, reason);
                }
            }
        }

        public bool LeaveLobby()
        {
            if (User.Instance.ID > 0)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new MainMenu());
                return true;
            }

            if (User.Instance.ID < 0)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new JoinLobby(), 650, 800);
                return true;
            }

            return false;
        }

        public void OnNewLobbyCreated(int lobbyId, int UserId)
        {
            _lobbyId = lobbyId;
            _users.Add(new UserLobby(User.Instance));
            CurrentLeaderId = UserId;
            Console.WriteLine(_lobbyId);

            tbLobbyCode.Text = lobbyId.ToString();
            lbUserName.Content = User.Instance.Username;
        }

        public void OnJoinLobby(int lobbyId, UserDTO userDto)
        {
            var user = new UserLobby(userDto);
            _users.Add(user);
            _lobbyId = lobbyId;
            tbLobbyCode.Text = lobbyId.ToString();
        }

        public void OnLeaveLobby(int lobbyId, int UserId)
        {
            UserLobby user = _users.FirstOrDefault(u => u.ID == UserId);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public void OnKicked(int lobbyId, string reason)
        {
            LeaveLobby();
            var notification = new NotificationDialog();
            notification.ShowErrorNotification(LangUtils.Translate("lblKickedFromLobby") + $": {reason}");
        }

        public void OnLeaderChanged(int lobbyId, int newLeaderId)
        {
            CurrentLeaderId = newLeaderId;
        }

        public void OnSendMessage(int UserId, string message)
        {
            UserLobby user = _users.FirstOrDefault(u => u.ID == UserId);

            if (user != null)
            {
                string username = user.Username;
                _messages.Add(new Message(username, message, _lobbyId));
            }
            else
            {
                _messages.Add(new Message(LangUtils.Translate("lblUser") + $" {UserId}", message, _lobbyId));
            }

            Dispatcher.BeginInvoke(new Action(() => { ScrollToBottom(); }),
                System.Windows.Threading.DispatcherPriority.Background);
        }

        public void OnLobbyUsersUpdate(int lobbyId, UserDTO[] users)
        {
            _lobbyId = lobbyId;

            _users.Clear();

            foreach (UserDTO userDto in users)
            {
                var user = new UserLobby(userDto);
                _users.Add(user);
            }

            foreach (UserLobby userDto in _users)
            {
                Console.WriteLine(userDto.Username);
            }

            if (!_users.Any(u => u.ID == User.Instance.ID))
            {
                _users.Add(new UserLobby(User.Instance));
            }

            tbLobbyCode.Text = lobbyId.ToString();
        }

        private void btInviteFriend_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLeader)
            {
                return;
            }

            var inviteFriends = new InviteFriends(_lobbyId);
            inviteFriends.ShowDialog();
        }

        private void ScrollToBottom()
        {
            var scrollViewer = FindVisualChild<ScrollViewer>(MessagesListBox);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToEnd();
            }
        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                {
                    return (T)child;
                }
                else
                {
                    var childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }

            return null;
        }

        public void GameStarted(int gameId)
        {
            GameLogic.Instance.GameId = gameId;

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var gameBoard = new GameBoard();
            mainWindow.NavigateToView(gameBoard, true);
        }

        public void OnReadyStatusChanged(int userId, bool isReady)
        {
            UserLobby user = _users.FirstOrDefault(u => u.ID == userId);
            if (user != null)
            {
                user.IsReady = isReady;
                Console.WriteLine(isReady);
            }
        }

        private void btReady_Click(object sender, RoutedEventArgs e)
        {
            _servicio.ChangeReadyStatus(_lobbyId, User.Instance.ID);
        }

    }

}