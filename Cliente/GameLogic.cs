using Cliente.Pantallas;
using Cliente.ServiceReference;
using Cliente.UserControllers;
using Haley.Utils;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Cliente
{


    public class GameLogic : INotifyPropertyChanged, IGameManagerCallback
    {


        public event Action<CardDTO> BodyPartSelectionRequested;
        public event Action<CardDTO> ToolSelectionRequested;
        public event Action<CardDTO> HatSelectionRequested;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private static GameLogic _instance;
        private static readonly object _lock = new object();
        private GameStateDTO _currentGameState;
        private string _lastCardDrawn;
        private int _currentPlayerId;
        private int _actionsRemaining;
        private int _turnTimeRemainingInSeconds;
        private int _playerActionsRemaining;
        private int _cardsRemainingInDeck;
        private string _player1Username;
        private string _player2Username;
        private string _player3Username;
        private string _player4Username;

        public string Player1Username
        {
            get => _player1Username;
            set 
            {  
                _player1Username = value;
                OnPropertyChanged(nameof(Player1Username));
            }
        }

        public string Player2Username
        {
            get => _player2Username;
            set
            {
                _player2Username = value;
                OnPropertyChanged(nameof(Player2Username));
            }
        }

        public string Player3Username
        {
            get => _player3Username;
            set
            {
                _player3Username = value;
                OnPropertyChanged(nameof(Player3Username));
            }
        }

        public string Player4Username
        {
            get => _player4Username;
            set
            {
                _player4Username = value;
                OnPropertyChanged(nameof(Player4Username));
            }
        }

        private DispatcherTimer _turnTimer;



        public List<int> playerMapping = new List<int>();
        public static GameLogic Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new GameLogic();
                        }
                    }
                }
                return _instance;
            }
        }
        public int GameId { get; set; }
        public ObservableCollection<GameCard> BabyDeck { get; set; } = new ObservableCollection<GameCard>();
        public ObservableCollection<CardDTO> Hand { get; set; } = new ObservableCollection<CardDTO>();
        public ObservableCollection<MonsterDTO> Monster { get; set; } = new ObservableCollection<MonsterDTO>();
        public ObservableCollection<GameCard> CardListViewer { get; set; } = new ObservableCollection<GameCard>();
        public ObservableCollection<GameMonster> Player1Monsters { get; set; } = new ObservableCollection<GameMonster>();
        public ObservableCollection<GameMonster> Player2Monsters { get; set; } = new ObservableCollection<GameMonster>();
        public ObservableCollection<GameMonster> Player3Monsters { get; set; } = new ObservableCollection<GameMonster>();
        public ObservableCollection<GameMonster> Player4Monsters { get; set; } = new ObservableCollection<GameMonster>();

        public int CardsRemainingInDeck
        {
            get => _cardsRemainingInDeck;
            set
            {
                _cardsRemainingInDeck = value;
                OnPropertyChanged(nameof(CardsRemainingInDeck));
            }
        }
        public string LastCardDrawn
        {
            get => _lastCardDrawn;
            set
            {
                _lastCardDrawn = value;
                OnPropertyChanged(nameof(LastCardDrawn));
            }
        }
        public int CurrentPlayerId
        {
            get => _currentPlayerId;
            set
            {
                _currentPlayerId = value;
                OnPropertyChanged(nameof(CurrentPlayerId));
            }
        }
        public int ActionsRemaining
        {
            get => _actionsRemaining;
            set
            {
                _actionsRemaining = value;
                OnPropertyChanged(nameof(ActionsRemaining));
            }
        }
        public int TurnTimeRemainingInSeconds
        {
            get => _turnTimeRemainingInSeconds;
            set
            {
                if (_turnTimeRemainingInSeconds != value)
                {
                    _turnTimeRemainingInSeconds = value;
                    OnPropertyChanged(nameof(TurnTimeRemainingInSeconds));
                }
            }
        }
        public int PlayerActionsRemaining
        {
            get => _playerActionsRemaining;
            set
            {
                if (_playerActionsRemaining != value)
                {
                    _playerActionsRemaining = value;
                    OnPropertyChanged(nameof(PlayerActionsRemaining));
                }
            }
        }
        public GameStateDTO CurrentGameState
        {
            get => _currentGameState;
            set
            {
                _currentGameState = value;
                OnPropertyChanged(nameof(CurrentGameState));
                UpdatePropertiesFromGameState(_currentGameState);
            }
        }

        

        public void ReceiveGameState(GameStateDTO gameState)
        {
            if (GameLogic.Instance == null)
            {
                Console.WriteLine("GameLogic null");
            }
            if (gameState == null)
            {
                Console.WriteLine("gameState Sent isNull");
            }

            else
            {
                Console.WriteLine("gameState Sent isNotNull");
            }

            CurrentGameState = gameState;

            Console.WriteLine(gameState.CurrentPlayerId);


            Player1Monsters.Clear();
            if (gameState.playerState.TryGetValue(User.Instance.ID, out var playerState))
            {
                Player1Username = playerState.User.Username;
                foreach (var monster in playerState.Monsters)
                {
                    Player1Monsters.Add(new GameMonster(monster));
                }
            }
            else
            {
                Console.WriteLine($"Player state not found for User ID: {User.Instance.ID}");
            }

            Player2Monsters.Clear();
            Player3Monsters.Clear();
            Player4Monsters.Clear();

            int playerNumber = 2;
            foreach (var player in gameState.playerState)
            {
                int playerId = player.Key;


                if (playerId == User.Instance.ID)
                    continue;

                switch (playerNumber)
                {
                    case 2:
                        Player2Username = player.Value.User.Username;
                        foreach (var monster in player.Value.Monsters)
                        {
                            Player2Monsters.Add(new GameMonster(monster));
                        }
                        break;

                    case 3:
                        Player3Username = player.Value.User.Username;
                        foreach (var monster in player.Value.Monsters)
                        {
                            Player3Monsters.Add(new GameMonster(monster));
                        }
                        break;

                    case 4:
                        Player4Username = player.Value.User.Username;
                        foreach (var monster in player.Value.Monsters)
                        {
                            Player4Monsters.Add(new GameMonster(monster));
                        }
                        break;

                    default:
                        Console.WriteLine($"No monster list available for Player {playerNumber}");
                        break;
                }

                playerNumber++;

                if (playerNumber > 4)
                    break;
            }


        }

        private void UpdatePropertiesFromGameState(GameStateDTO gameState)
        {
            Console.WriteLine("Estoy cambiando??");
            if (gameState == null)
                return;

            GameId = gameState.GameStateId;
            CurrentPlayerId = gameState.CurrentPlayerId;
            ActionsRemaining = gameState.PlayerActionsRemaining.ContainsKey(User.Instance.ID) ? gameState.PlayerActionsRemaining[User.Instance.ID] : 0;
            TurnTimeRemainingInSeconds = gameState.TurnTimeRemainingInSeconds;

            StartTurnTimer();


            BabyDeck.Clear();
            foreach (var card in gameState.BabyDeck)
            {
                BabyDeck.Add(new GameCard(card));
            }


            var currentPlayerState = gameState.playerState
                .FirstOrDefault(ps => ps.Key == CurrentPlayerId).Value;


            var newCard = gameState.playerState.FirstOrDefault(ps => ps.Key == User.Instance.ID).Value.Hand.Last(); ;
            LastCardDrawn = newCard != null ? $"Card ID: {newCard.CardId}" : "No card drawn";
            Console.WriteLine(LastCardDrawn);

            CardListViewer.Clear();
            foreach (var card in gameState.playerState[User.Instance.ID].Hand)
            {
                CardListViewer.Add(new GameCard(card));
            }

            Monster.Clear();
            if (gameState.playerState.TryGetValue(User.Instance.ID, out var playerState))
            {
                foreach (var monster in playerState.Monsters)
                {
                    Monster.Add(monster);
                }
            }

            if (gameState.PlayerActionsRemaining.TryGetValue(User.Instance.ID, out int actionsRemaining))
            {
                PlayerActionsRemaining = actionsRemaining;
            }
            else
            {
                PlayerActionsRemaining = 0;
            }


            CardsRemainingInDeck = gameState.CardsRemainingInDeck;

        }
        public void RequestBodyPartSelection(int userId, int matchCode, CardDTO card)
        {
            BodyPartSelectionRequested?.Invoke(card);
        }
        public void RequestToolSelection(int userId, int matchCode, CardDTO card)
        {
            ToolSelectionRequested?.Invoke(card);
        }
        public void RequestHatSelection(int userId, int matchCode, CardDTO card)
        {
            HatSelectionRequested?.Invoke(card);
        }
        public void RequestProvokeSelection(int userId, int matchCode)
        {
            throw new NotImplementedException();
        }
        private void StartTurnTimer()
        {
            _turnTimer?.Stop();

            _turnTimer = new DispatcherTimer();
            _turnTimer.Interval = TimeSpan.FromSeconds(1);
            _turnTimer.Tick += TurnTimer_Tick;

            _turnTimer.Start();
        }
        private void TurnTimer_Tick(object sender, EventArgs e)
        {
            if (TurnTimeRemainingInSeconds > 0)
            {
                TurnTimeRemainingInSeconds--;
            }
            else
            {

                NotificationDialog notification = new NotificationDialog();

            }
        }
        public void NotifyActionInvalid(string messageKey)
        {
            NotificationDialog notification = new NotificationDialog();

            notification.ShowInfoNotification(LangUtils.Translate(messageKey));
        }
    }
}


