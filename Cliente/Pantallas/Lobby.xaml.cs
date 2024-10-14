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

        private LobbyManagerClient _servicio;



        public Lobby()
        {
            InitializeComponent();

            InstanceContext instanceContext = new InstanceContext(this);
            _servicio = new LobbyManagerClient(instanceContext);
            _messages = new ObservableCollection<Message>();
            MessagesListBox.ItemsSource = _messages;
            


        }

        private void btLeaveLobby_Click(object sender, RoutedEventArgs e)
        {

            string username = tbLobbyCode.Text;
            string lobbyCode = codigoSala.Text;



            if (result)
            {
                LeaveLobby();    
            }
            else
            {
                lbErrGeneral.Content = "No se puede salir de la sala, intentalo de nuevo";
            }
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbLobbyCode.Text;
            string lobbyCode = codigoSala.Text;

           

            if (connected)
            {
                JoinLobby();
            }
            else
            {
                lbErrGeneral.Content = "No se puede unir a la sala, intentalo de nuevo";
            }
        }


        private void btSendMessage_Click(object sender, RoutedEventArgs e)
        {
                

        }

        private void btStartGame_Click(object sender, RoutedEventArgs e)
        {

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
            throw new NotImplementedException();
        }

        public void OnJoinLobby(int lobbyId, int UserId)
        {
            throw new NotImplementedException();
        }

        public void OnLeaveLobby(int lobbyId, int UserId)
        {
            throw new NotImplementedException();
        }

        public void OnSendMessage(int UserId, string message)
        {
            throw new NotImplementedException();
        }
    }
}
