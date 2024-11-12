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

namespace Cliente.GameUserControllers
{
    /// <summary>
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoard : UserControl
    {
        private List<int> playerMapping = new List<int>();

        public GameManagerClient gameManagerClient;

        public GameBoard()
        {
            InitializeComponent();
            this.DataContext = GameLogic.Instance;

            this.Loaded += GameBoard_Loaded;

            var cardsViewer = new CardsViewer();
            extensiblePanelContentControl.Content = cardsViewer;

        }

        private void GameBoard_Loaded(object sender, RoutedEventArgs e)
        {
            gameManagerClient = new GameManagerClient(new InstanceContext(GameLogic.Instance));

            try
            {
                UserDto user = new UserDto();
                user.UserId = User.Instance.ID;
                user.Username = User.Instance.Username;
                user.Email = User.Instance.Email;
                user.ProfilePictureId = User.Instance.ProfilePictureId;

                gameManagerClient.JoinGame(GameLogic.Instance.GameId, user);
            }
            catch (CommunicationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void MapPlayers()
        {
            foreach(var player in GameLogic.Instance.CurrentGameState.playerState)
            {
                if (player.Value.User.UserId != User.Instance.ID)
                {
                    playerMapping.Add(player.Value.User.UserId);
                }

            }
        }

        private void DrawThisFuckingCard(object sender, RoutedEventArgs e)
        {
            gameManagerClient.DrawCard(GameLogic.Instance.GameId, User.Instance.ID);
            GameLogic.Instance.CardListViewer.Add(new GameCard("pack://application:,,,/Cards/Card2.jpg"));
        }

        private void imgPlayer1Monster_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgPlayer1Cards_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
