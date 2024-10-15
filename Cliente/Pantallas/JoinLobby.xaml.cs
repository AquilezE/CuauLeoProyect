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
            Lobby lobbyWindow = new Lobby();

            UserDto userDto = new UserDto();
            userDto.UserId = User.Instance.ID;
            userDto.Username = User.Instance.Username;
            userDto.Email = User.Instance.Email;
            userDto.ProfilePictureId = User.Instance.ProfilePictureId;

            lobbyWindow._servicio.JoinLobby(int.Parse(tbLobbyCode.Text), userDto);

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToMensajeador(lobbyWindow);
        }

        private void btGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToMensajeador(new MainMenu());
        }
    }
}
