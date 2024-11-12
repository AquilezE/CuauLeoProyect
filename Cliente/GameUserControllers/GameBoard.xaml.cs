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

        public GameManagerClient gameManagerClient;

        public GameBoard()
        {
            InitializeComponent();
            this.DataContext = GameLogic.Instance;

            this.Loaded += GameBoard_Loaded;

            var cardsViewer = new CardsViewer();
            extensiblePanelContentControl.Content = cardsViewer;

            this.AddHandler(CardUserController.CardClickedEvent, new RoutedEventHandler(Card_Clicked));

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

        private void Card_Clicked(object sender, RoutedEventArgs e)
        {
            var cardControl = e.OriginalSource as CardUserController;
            if (cardControl != null)
            {
                var cardData = cardControl.DataContext;
                
                var card = cardData as GameCard;

                gameManagerClient.PlayCard(User.Instance.ID,GameLogic.Instance.GameId,card.CardId);

                Console.WriteLine($"Card clicked: {card.CardId}");
            }
        }

        private void DrawCard(object sender, RoutedEventArgs e)
        {
            gameManagerClient.DrawCard(GameLogic.Instance.GameId, User.Instance.ID);
        }

        private void imgPlayer1Monster_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void imgPlayer1Cards_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgBabieLando_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgBabieWata_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgBabieAir_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
