using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Cliente.UserControllers;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Haley.Utils;
using System.ComponentModel;
using Cliente.GameUserControllers;

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
        private ObservableCollection<User> _users;


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

            InstanceContext instanceContext = new InstanceContext(this);
            _servicio = new LobbyManagerClient(instanceContext);
            _messages = new ObservableCollection<Message>();
            _users = new ObservableCollection<User>();
            MessagesListBox.ItemsSource = _messages;
            UsersListBox.ItemsSource = _users;
            lbUserName.Content = User.Instance.Username;
            
        }

        private void btLeaveLobby_Click(object sender, RoutedEventArgs e)
        {
            _servicio.LeaveLobby(_lobbyId, User.Instance.ID);
            LeaveLobby();
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

                //Needa shit ton of validation i'm afraid
                _servicio.StartGame(_lobbyId);

            }
        }

        private void UserLobby_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is UserLobby userLobbyControl)
            {
                userLobbyControl.KickRequested += OnKickRequested;
            }
        }

        private void OnKickRequested(object sender, User userToKick)
        {
            if (userToKick != null)
            {
                KickUserDialog kickDialog = new KickUserDialog(userToKick.Username)
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
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToView(new MainMenu());
            return true;
        }

        public void OnNewLobbyCreated(int lobbyId, int UserId)
        {

            
            _lobbyId = lobbyId;
            _users.Add(User.instance);
            CurrentLeaderId = UserId;
            Console.WriteLine(_lobbyId);

            tbLobbyCode.Text = lobbyId.ToString();
            lbUserName.Content = User.Instance.Username;


        }

        public void OnJoinLobby(int lobbyId, UserDto userDto)
        {
            User user = new User(userDto);
            _users.Add(user);
            _lobbyId = lobbyId;
            tbLobbyCode.Text = lobbyId.ToString();
        }

        public void OnLeaveLobby(int lobbyId, int UserId)
        {
            var user = _users.FirstOrDefault(u => u.ID == UserId);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public void OnKicked(int lobbyId, string reason)
        {
            LeaveLobby();
            NotificationDialog notification = new NotificationDialog();
            //INTERNATIONALIZATION NEEDED
            notification.ShowErrorNotification($"You were kicked from the lobby: {reason}");

        }

        public void OnLeaderChanged(int lobbyId, int newLeaderId)
        {
            CurrentLeaderId = newLeaderId;
        }

        public void OnSendMessage(int UserId, string message)
        {

            User user = _users.FirstOrDefault(u => u.ID == UserId);

            if (user != null)
            {
                string username = user.Username;
                _messages.Add(new Message(username, message, _lobbyId));
            }
            else
            {
                //INTERNATIONALIZATION NEEDED
                _messages.Add(new Message($"User {UserId}", message, _lobbyId));
            }

            Dispatcher.BeginInvoke(new Action(() =>
            {
                ScrollToBottom();
            }), System.Windows.Threading.DispatcherPriority.Background);

        }

        public void OnLobbyUsersUpdate(int lobbyId, UserDto[] users)
        {
            _lobbyId = lobbyId;

            _users.Clear();

            foreach (var userDto in users)
            {
                User user = new User(userDto);
                _users.Add(user);
            }

            foreach (var userDto in _users)
            {
                Console.WriteLine(userDto.Username);
            }

            if (!_users.Any(u => u.ID == User.Instance.ID))
            {
                _users.Add(User.Instance);
            }

            tbLobbyCode.Text = lobbyId.ToString();
        }

        private void btKick_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ScrollToBottom()
        {
            ScrollViewer scrollViewer = FindVisualChild<ScrollViewer>(MessagesListBox);
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
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }



        public void GameStarted(int gameId)
        {
            GameLogic.Instance.GameId = gameId;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            GameBoard gameBoard = new GameBoard();
            mainWindow.NavigateToView(gameBoard);

        }

        private void btInviteFriend_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLeader)
            {
                return;
            }
            InviteFriends inviteFriends = new InviteFriends(_lobbyId);
            inviteFriends.ShowDialog();

        }
    }
}
