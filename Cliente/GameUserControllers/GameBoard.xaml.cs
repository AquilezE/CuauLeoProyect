using Cliente.ServiceReference;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cliente.Pantallas;
using UserDTO = Cliente.ServiceReference.UserDTO;
using Cliente.UserControllers;
using Cliente.Utils;
using Haley.Utils;
using System.Linq.Expressions;

namespace Cliente.GameUserControllers
{

    public partial class GameBoard : UserControl
    {

        public GameManagerClient GameManagerClient;

        public GameBoard()
        {
            InitializeComponent();
            DataContext = GameLogic.Instance;

            Loaded += GameBoard_Loaded;

            var cardsViewer = new CardsViewer();
            extensiblePanelCards.Content = cardsViewer;

            var monstersViewer1 = new MonstersViewerPlayer1();
            extensiblePanelMonstersPlayer1.Content = monstersViewer1;

            var monstersViewer2 = new MonstersViewerPlayer2();
            extensiblePanelMonstersPlayer2.Content = monstersViewer2;

            var monstersViewer3 = new MonstersViewerPlayer3();
            extensiblePanelMonstersPlayer3.Content = monstersViewer3;

            var monstersViewer4 = new MonstersViewerPlayer4();
            extensiblePanelMonstersPlayer4.Content = monstersViewer4;

            AddHandler(CardUserController.CardClickedEvent, new RoutedEventHandler(Card_Clicked));


            GameLogic.Instance.BodyPartSelectionRequested += OnBodyPartSelectionRequested;
            GameLogic.Instance.ToolSelectionRequested += OnToolSelectionRequested;
            GameLogic.Instance.HatSelectionRequested += OnHatSelectionRequested;
            GameLogic.Instance.GameHasEnded += OnGameHasEnded;
            GameLogic.Instance.GameHasEndedWithoutUsers += OnGameHasEndedWithoutUsers;
        }

        private void GameBoard_Loaded(object sender, RoutedEventArgs e)
        {
            GameManagerClient = new GameManagerClient(new InstanceContext(GameLogic.Instance));
            var clientChannel = (ICommunicationObject)GameManagerClient;
            clientChannel.Closed += ClientChannel_Closed;
            clientChannel.Faulted += ClientChannel_Faulted;
            try
            {
                var user = new UserDTO();
                user.UserId = User.Instance.ID;
                user.Username = User.Instance.Username;
                user.Email = User.Instance.Email;
                user.ProfilePictureId = User.Instance.ProfilePictureId;

                GameManagerClient.JoinGame(GameLogic.Instance.GameId, user);
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
        }

        private void ClientChannel_Faulted(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (User.Instance.ID > 0)
                {
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrLostGameConnection"));
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(new MainMenu());
                }
                else if (User.Instance.ID < 0)
                {
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrLostGameConnection"));
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(new JoinLobby(), 650, 800);
                }
            });
        }

        private void ClientChannel_Closed(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (User.Instance.ID > 0)
                {
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrLostGameConnection"));
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(new MainMenu());
                }
                else if (User.Instance.ID < 0)
                {
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrLostGameConnection"));
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(new JoinLobby(), 650, 800);
                }
            });
        }

        private void Card_Clicked(object sender, RoutedEventArgs e)
        {
            var cardControl = e.OriginalSource as CardUserController;
            if (cardControl != null)
            {
                try
                {
                    object cardData = cardControl.DataContext;

                    var card = cardData as GameCard;

                    GameManagerClient.PlayCard(User.Instance.ID, GameLogic.Instance.GameId, card.CardId);
                }
                catch (EndpointNotFoundException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                }
                catch (CommunicationException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                }
                catch (TimeoutException ex)
                {
                    ExceptionManager.LogErrorException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
                }
                catch (Exception ex)
                {
                    ExceptionManager.LogFatalException(ex);
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                }
            }
        }

        private void DrawCard(object sender, RoutedEventArgs e)
        {
            try
            {
                GameManagerClient.DrawCard(GameLogic.Instance.GameId, User.Instance.ID);
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
        }


        private void extensiblePanelMonstersPlayer1_Loaded(object sender, RoutedEventArgs e)
        {
            if (extensiblePanelMonstersPlayer1.Content is MonstersViewerPlayer1 monstersViewer)
            {
                monstersViewer.ClosePanel += OnClosePanelPlayer1;
            }
            else
            {
                Console.WriteLine("Content not initialized or not of type MonstersViewerVertical.");
            }
        }

        private void extensiblePanelMonstersPlayer2_Loaded(object sender, RoutedEventArgs e)
        {
            if (extensiblePanelMonstersPlayer2.Content is MonstersViewerPlayer2 monstersViewerVertical)
            {
                monstersViewerVertical.ClosePanel += OnClosePanelPLayer2;
            }
            else
            {
                Console.WriteLine("Content not initialized or not of type MonstersViewerVertical.");
            }
        }

        private void extensiblePanelMonstersPlayer3_Loaded(object sender, RoutedEventArgs e)
        {
            if (extensiblePanelMonstersPlayer3.Content is MonstersViewerPlayer3 monstersViewerVertical)
            {
                monstersViewerVertical.ClosePanel += OnClosePanelPlayer3;
            }
            else
            {
                Console.WriteLine("Content not initialized or not of type MonstersViewerVertical.");
            }
        }

        private void extensiblePanelMonstersPlayer4_Loaded(object sender, RoutedEventArgs e)
        {
            if (extensiblePanelMonstersPlayer4.Content is MonstersViewerPlayer4 monstersViewerVertical)
            {
                monstersViewerVertical.ClosePanel += OnClosePanelPlayer4;
            }
            else
            {
                Console.WriteLine("Content not initialized or not of type MonstersViewerVertical.");
            }
        }

        private void OnToolSelectionRequested(CardDTO card)
        {
            Dispatcher.Invoke(() =>
            {
                var selectionWindow = new MonsterSelectionWindow(GameLogic.Instance.Monster);
                if (selectionWindow.ShowDialog() == true)
                {
                    try
                    {
                        int selectedMonsterIndex = selectionWindow.SelectedMonsterIndex;
                        GameManagerClient.ExecuteToolPlacement(User.Instance.ID, GameLogic.Instance.GameId, card.CardId,
                            selectedMonsterIndex);
                    }
                    catch (EndpointNotFoundException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    }
                    catch (CommunicationException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    }
                    catch (TimeoutException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.LogFatalException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    }
                }
            });
        }

        private void OnBodyPartSelectionRequested(CardDTO card)
        {
            Dispatcher.Invoke(() =>
            {
                var selectionWindow = new MonsterSelectionWindow(GameLogic.Instance.Monster);
                if (selectionWindow.ShowDialog() == true)
                {
                    try
                    {
                        int selectedMonsterIndex = selectionWindow.SelectedMonsterIndex;
                        GameManagerClient.ExecuteBodyPartPlacement(User.Instance.ID, GameLogic.Instance.GameId, card.CardId,
                            selectedMonsterIndex);
                    }
                    catch (EndpointNotFoundException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    }
                    catch (CommunicationException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    }
                    catch (TimeoutException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.LogFatalException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    }
                }
            });
        }

        private void OnHatSelectionRequested(CardDTO card)
        {
            Dispatcher.Invoke(() =>
            {
                var selectionWindow = new MonsterSelectionWindow(GameLogic.Instance.Monster);
                if (selectionWindow.ShowDialog() == true)
                {
                    try
                    {
                        int selectedMonsterIndex = selectionWindow.SelectedMonsterIndex;
                        GameManagerClient.ExecuteHatPlacement(User.Instance.ID, GameLogic.Instance.GameId, card.CardId,
                            selectedMonsterIndex);
                    }
                    catch (EndpointNotFoundException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    }
                    catch (CommunicationException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    }
                    catch (TimeoutException ex)
                    {
                        ExceptionManager.LogErrorException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.LogFatalException(ex);
                        var notificationDialog = new NotificationDialog();
                        notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
                    }
                }
            });
        }

        private void OnGameHasEnded(StatsDTO[] matchStats)
        {
            Dispatcher.Invoke(() =>
            {
                var channelClient = (ICommunicationObject)GameManagerClient;
                channelClient.Closed -= ClientChannel_Closed;
                channelClient.Faulted -= ClientChannel_Faulted;
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.NavigateToView(new EndGame(matchStats), 800, 750);
            });
        }

        private void OnGameHasEndedWithoutUsers(int matchCode)
        {
            Dispatcher.Invoke(() =>
            {
                if (User.Instance.ID > 0)
                {
                    var channelClient = (ICommunicationObject)GameManagerClient;
                    channelClient.Closed -= ClientChannel_Closed;
                    channelClient.Faulted -= ClientChannel_Faulted;
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblEndGameWhitoutUsers"));
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(new MainMenu());
                }
                else if (User.Instance.ID < 0)
                {
                    var channelClient = (ICommunicationObject)GameManagerClient;
                    channelClient.Closed -= ClientChannel_Closed;
                    channelClient.Faulted -= ClientChannel_Faulted;
                    var notificationDialog = new NotificationDialog();
                    notificationDialog.ShowErrorNotification(LangUtils.Translate("lblEndGameWhitoutUsers"));
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.NavigateToView(new JoinLobby(), 650, 800);
                }
            });
        }

        private void OnClosePanelPlayer1(object sender, MonstersViewerPlayer1 e)
        {
            extensiblePanelMonstersPlayer1.Margin = new Thickness(0, -12200, -12200, 0);
        }

        private void OnClosePanelPLayer2(object sender, MonstersViewerPlayer2 e)
        {
            extensiblePanelMonstersPlayer2.Margin = new Thickness(0, -12200, -12200, 0);
        }

        private void OnClosePanelPlayer3(object sender, MonstersViewerPlayer3 e)
        {
            extensiblePanelMonstersPlayer3.Margin = new Thickness(0, -12200, -12200, 0);
        }

        private void OnClosePanelPlayer4(object sender, MonstersViewerPlayer4 e)
        {
            extensiblePanelMonstersPlayer4.Margin = new Thickness(0, -12200, -12200, 0);
        }

        private void btnPlayer1Monster_Click(object sender, RoutedEventArgs e)
        {
            extensiblePanelMonstersPlayer1.Margin = new Thickness(0, 296, 0, 0);
        }

        private void btnPlayer2Monster_Click(object sender, RoutedEventArgs e)
        {
            extensiblePanelMonstersPlayer2.Margin = new Thickness(0, 19, 0, 277);
        }

        private void btnPlayer3Monster_Click(object sender, RoutedEventArgs e)
        {
            extensiblePanelMonstersPlayer3.Margin = new Thickness(971, 0, 0, 0);
        }

        private void btnPlayer4Monster_Click(object sender, RoutedEventArgs e)
        {
            extensiblePanelMonstersPlayer4.Margin = new Thickness(0, 0, 971, 0);
        }

        private async void btnProvokeEarth_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                await GameManagerClient.PlayProvokeAsync(User.Instance.ID, GameLogic.Instance.GameId, 0);
                Console.WriteLine("Provoke Water");
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
        }
        

        private async void btnProvokeWater_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await GameManagerClient.PlayProvokeAsync(User.Instance.ID, GameLogic.Instance.GameId, 1);
                Console.WriteLine("Provoke Water");
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
        }

        private async void btnProvokeSky_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await GameManagerClient.PlayProvokeAsync(User.Instance.ID, GameLogic.Instance.GameId, 2);
                Console.WriteLine("Provoke Water");
            }
            catch (EndpointNotFoundException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (CommunicationException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
            catch (TimeoutException ex)
            {
                ExceptionManager.LogErrorException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrTimeout"));
            }
            catch (Exception ex)
            {
                ExceptionManager.LogFatalException(ex);
                var notificationDialog = new NotificationDialog();
                notificationDialog.ShowErrorNotification(LangUtils.Translate("lblErrNoConection"));
            }
        }

    }

}