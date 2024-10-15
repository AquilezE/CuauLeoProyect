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

        //Public?????
        public LobbyManagerClient _servicio;

        private int _lobbyId;



        public Lobby()
        {
            InitializeComponent();

            InstanceContext instanceContext = new InstanceContext(this);
            
            _servicio = new LobbyManagerClient(instanceContext);
            _messages = new ObservableCollection<Message>();
            _users = new ObservableCollection<User>();
            MessagesListBox.ItemsSource = _messages;
            
        }

        private void btLeaveLobby_Click(object sender, RoutedEventArgs e)
        {


        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btSendMessage_Click(object sender, RoutedEventArgs e)
        {
            string message = tbMessage.Text.Trim();

            if (!IsValidMessage(message))
            {
                lbErrGeneral.Content = _lobbyId.ToString();
                return;

            }
            
            Console.WriteLine($"_lobbyId in btSendMessage_Click: {_lobbyId}");
            Console.WriteLine($"User.Instance.ID in btSendMessage_Click: {User.Instance.ID}");
            Console.WriteLine($"message in btSendMessage_Click: {message}");
            _servicio.SendMessage(_lobbyId, User.Instance.ID, message);
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

        private void btKick_Click(object sender, RoutedEventArgs e)
        {

        }

        public bool LeaveLobby()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToMensajeador(new MainMenu());
            return true;
        }

        public void OnNewLobbyCreated(int lobbyId, int UserId)
        {

            
            _lobbyId = lobbyId;
            _users.Add(User.instance);
            Console.WriteLine(_lobbyId);

            tbLobbyCode.Text = lobbyId.ToString();
            
        }

        public void OnJoinLobby(int lobbyId, UserDto userDto)
        {
            User user = new User(userDto);
            _users.Add(user);
            Console.WriteLine("OnJoinLobby called");
            Console.WriteLine($"userDto in OnJoinLobby: {userDto.Username}");

            _lobbyId = lobbyId;
            tbLobbyCode.Text = lobbyId.ToString();

            
        }

        public void OnLeaveLobby(int lobbyId, int UserId)
        {
            throw new NotImplementedException();
        }

        public void OnSendMessage(int UserId, string message)
        {
            Console.WriteLine("OnSendMessage called");
            Console.WriteLine($"message in OnSendMessage: {message}");

            User user = _users.FirstOrDefault(u => u.ID == UserId);

            if (user != null)
            {
                string username = user.Username;
                _messages.Add(new Message(username, message, _lobbyId));
            }
            else
            {
                // Handle the case where the user is not found
                // For example, you might add the message with a placeholder username
                _messages.Add(new Message($"User {UserId}", message, _lobbyId));
            }
        }
    }
}
