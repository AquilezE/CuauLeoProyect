using Cliente.Service1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


    public partial class Lobby : UserControl, Service1.ILobbyCallback
    {

        private Service1.LobbyClient _servicio;
        private ObservableCollection<Message> _messages;


        public Lobby()
        {
            InitializeComponent();

            _messages = new ObservableCollection<Message>();
            MessagesListBox.ItemsSource = _messages;
            
            _servicio = new Service1.LobbyClient(new System.ServiceModel.InstanceContext(this));


        }

        public void GetMessage(Service1.Message message)
        {
            Message newMessage = new Message { UserName = message.UserName, Text = message.Text};
            _messages.Add(newMessage);
        }

        private void btLeaveLobby_Click(object sender, RoutedEventArgs e)
        {

            string username = tbLobbyCode.Text;
            string lobbyCode = codigoSala.Text;


            bool result = _servicio.Disconnect(lobbyCode, username);

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

            bool connected = _servicio.Connect(lobbyCode, username);
           

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
            string Text = tbMessage.Text;

            if (Text != "")
            {

               Service1.Message newMessage = new Service1.Message{ UserName = tbLobbyCode.Text, Text = Text, LobbyCode = codigoSala.Text};
                tbMessage.Text = "";
               _servicio.SendMessage(newMessage);
            }
            else
            {
                lbMessageError.Content = "No se puede enviar un mensaje vacio";
            }


        }

        private void btStartGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btKick_Click(object sender, RoutedEventArgs e)
        {

        }

        public bool JoinLobby()
        {
            lbErrGeneral.Content = "Te Uniste a la sala";
            return true;
        }


        public bool LeaveLobby()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToMensajeador(new MenuPrincipal());
            return true;
        }


    }
}
