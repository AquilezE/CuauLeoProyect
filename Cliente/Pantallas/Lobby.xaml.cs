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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.Pantallas
{


    public partial class Lobby : UserControl, ILobbyManagerCallback
    {

        private ObservableCollection<Message> _messages;
        private ObservableCollection<User> _users;


        public LobbyManagerClient _servicio;
        private int _lobbyId;
        
        private int _currentLeaderId;
        private bool _isLeader => _currentLeaderId == User.Instance.ID;


        public Lobby()
        {
            InitializeComponent();

            InstanceContext instanceContext = new InstanceContext(this);
            
            _servicio = new LobbyManagerClient(instanceContext);
            _messages = new ObservableCollection<Message>();
            _users = new ObservableCollection<User>();
            MessagesListBox.ItemsSource = _messages;
            UsersListBox.ItemsSource = _users;
            tbUserName.Text = User.Instance.Username;
            
        }

        private void btLeaveLobby_Click(object sender, RoutedEventArgs e)
        {
            _servicio.LeaveLobby(_lobbyId, User.Instance.ID);
            LeaveLobby();
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
       {
            throw new NotImplementedException();
        }


        private void btSendMessage_Click(object sender, RoutedEventArgs e)
        {
            string message = tbMessage.Text.Trim();

            if (!IsValidMessage(message))
            {
                lbErrGeneral.Content = _lobbyId.ToString();
                return;

            }

            _servicio.SendMessage(_lobbyId, User.Instance.ID, message);
            tbMessage.Text = string.Empty;
        }

        private bool IsValidMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return false;
            }

            if (message.Length > 200)
            {
                return false;
            }

            return true;
        }

        private void btStartGame_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("btStartGame_Click called");
            Console.WriteLine($"_lobbyId in btStartGame_Click: {_lobbyId}");
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
                var result = MessageBox.Show($"Are you sure you want to kick {userToKick.Username}?", "Kick User", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    _users.Remove(userToKick);

                    _servicio.KickUser(_lobbyId, User.Instance.ID, userToKick.ID, "You have been kicked.");
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
            Console.WriteLine(_lobbyId);

            tbLobbyCode.Text = lobbyId.ToString();
            tbUserName.Text = User.Instance.Username;


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
            MessageBox.Show($"You were kicked from the lobby: {reason}", "Kicked", MessageBoxButton.OK, MessageBoxImage.Information);
            LeaveLobby();
        }



        public void OnLeaderChanged(int lobbyId, int newLeaderId)
        {
            _currentLeaderId = newLeaderId;
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
                _messages.Add(new Message($"User {UserId}", message, _lobbyId));
            }
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
    }
}
