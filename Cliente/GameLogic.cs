using Cliente.GameUserControllers;
using Cliente.ServiceReference;
using Cliente.UserControllers;
using Haley.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Windows.Threading;

namespace Cliente
{
    public class GameLogic : INotifyPropertyChanged, IGameManagerCallback
    {
        public event Action<CardDTO> BodyPartSelectionRequested;
        public event Action<CardDTO> ToolSelectionRequested;
        public event Action<CardDTO> HatSelectionRequested;
        public event Action<StatsDTO[]> GameHasEnded;
        public event Action<int> GameHasEndedWithoutUsers;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
        private int _player1Score;
        private int _player2Score;
        private int _player3Score;
        private int _player4Score;
        private DispatcherTimer _turnTimer;


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

        public int Player1Score
        {
            get => _player1Score;
            set
            {
                _player1Score = value;
                OnPropertyChanged(nameof(Player1Score));
            }
        }

        public int Player2Score
        {
            get => _player2Score;
            set
            {
                _player2Score = value;
                OnPropertyChanged(nameof(Player2Score));
            }
        }

        public int Player3Score
        {
            get => _player3Score;
            set
            {
                _player3Score = value;
                OnPropertyChanged(nameof(Player3Score));
            }
        }

        public int Player4Score
        {
            get => _player4Score;
            set
            {
                _player4Score = value;
                OnPropertyChanged(nameof(Player4Score));
            }
        }


        public static GameLogic Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null) _instance = new GameLogic();
                    }
                }

                return _instance;
            }
        }

        public static GameLogic ResetInstance()
        {
            _instance = null;
            return Instance;
        }

        public int GameId { get; set; }
        public ObservableCollection<GameCard> BabyDeck { get; set; } = new ObservableCollection<GameCard>();
        public ObservableCollection<CardDTO> Hand { get; set; } = new ObservableCollection<CardDTO>();
        public ObservableCollection<MonsterDTO> Monster { get; set; } = new ObservableCollection<MonsterDTO>();
        public ObservableCollection<GameCard> CardListViewer { get; set; } = new ObservableCollection<GameCard>();

        public ObservableCollection<GameMonster> Player1Monsters { get; set; } =
            new ObservableCollection<GameMonster>();

        public ObservableCollection<GameMonster> Player2Monsters { get; set; } =
            new ObservableCollection<GameMonster>();

        public ObservableCollection<GameMonster> Player3Monsters { get; set; } =
            new ObservableCollection<GameMonster>();

        public ObservableCollection<GameMonster> Player4Monsters { get; set; } =
            new ObservableCollection<GameMonster>();


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
            CurrentGameState = gameState;

            Console.WriteLine(gameState.CurrentPlayerId);


            Player1Monsters.Clear();
            if (gameState.PlayerState.TryGetValue(User.Instance.ID, out PlayerStateDTO playerState))
            {
                Player1Score = gameState.PlayerStatistics[User.Instance.ID].Points;
                Player1Username = playerState.User.Username;
                foreach (MonsterDTO monster in playerState.Monsters) Player1Monsters.Add(new GameMonster(monster));
            }


            Player2Monsters.Clear();
            Player3Monsters.Clear();
            Player4Monsters.Clear();

            int playerNumber = 2;
            foreach (KeyValuePair<int, PlayerStateDTO> player in gameState.PlayerState)
            {
                int playerId = player.Key;


                if (playerId == User.Instance.ID)
                    continue;

                switch (playerNumber)
                {
                    case 2:
                        Player2Score = gameState.PlayerStatistics[playerId].Points;
                        Player2Username = player.Value.User.Username;
                        foreach (MonsterDTO monster in player.Value.Monsters)
                            Player2Monsters.Add(new GameMonster(monster));
                        break;

                    case 3:
                        Player3Score = gameState.PlayerStatistics[playerId].Points;
                        Player3Username = player.Value.User.Username;
                        foreach (MonsterDTO monster in player.Value.Monsters)
                            Player3Monsters.Add(new GameMonster(monster));
                        break;

                    case 4:
                        Player4Score = gameState.PlayerStatistics[playerId].Points;
                        Player4Username = player.Value.User.Username;
                        foreach (MonsterDTO monster in player.Value.Monsters)
                            Player4Monsters.Add(new GameMonster(monster));
                        break;

                    default:
                        break;
                }

                playerNumber++;

                if (playerNumber > 4)
                    break;
            }
        }

        private void UpdatePropertiesFromGameState(GameStateDTO gameState)
        {
            if (gameState == null)
                return;

            GameId = gameState.GameStateId;
            CurrentPlayerId = gameState.CurrentPlayerId;
            ActionsRemaining = gameState.PlayerActionsRemaining.ContainsKey(User.Instance.ID)
                ? gameState.PlayerActionsRemaining[User.Instance.ID]
                : 0;
            TurnTimeRemainingInSeconds = gameState.TurnTimeRemainingInSeconds;

            StartTurnTimer();


            BabyDeck.Clear();
            foreach (CardDTO card in gameState.BabyDeck) BabyDeck.Add(new GameCard(card));


            CardDTO newCard = gameState.PlayerState.FirstOrDefault(ps => ps.Key == User.Instance.ID).Value.Hand.Last();
            ;
            LastCardDrawn = newCard != null ? $"Card ID: {newCard.CardId}" : "No card drawn";
            Console.WriteLine(LastCardDrawn);

            CardListViewer.Clear();
            foreach (CardDTO card in gameState.PlayerState[User.Instance.ID].Hand)
                CardListViewer.Add(new GameCard(card));

            Monster.Clear();
            if (gameState.PlayerState.TryGetValue(User.Instance.ID, out PlayerStateDTO playerState))
                foreach (MonsterDTO monster in playerState.Monsters)
                    Monster.Add(monster);

            if (gameState.PlayerActionsRemaining.TryGetValue(User.Instance.ID, out int actionsRemaining))
                PlayerActionsRemaining = actionsRemaining;
            else
                PlayerActionsRemaining = 0;


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
                TurnTimeRemainingInSeconds--;
            else
            {
                var notification = new NotificationDialog();
            }
        }

        public void NotifyActionInvalid(string messageKey)
        {
            var notification = new NotificationDialog();

            notification.ShowInfoNotification(LangUtils.Translate(messageKey));
        }

        public void OnProvoke(int matchCode, int babyPileIndex)
        {
            var animationWindow = new ProvokeAnimationWindow();

            animationWindow.ShowDialog();
        }

        public void OnNotifyEndGamePhase()
        {
            var animationWindow = new EndGameStartedAnimationWindow();
            animationWindow.ShowDialog();
        }

        public void OnNotifyGameEnded(int matchCode, StatsDTO[] gameStats)
        {
            GameHasEnded?.Invoke(gameStats);
            var orderedStats = new List<Stats>();
            foreach (StatsDTO stats in gameStats) orderedStats.Add(new Stats(stats));

            orderedStats = orderedStats.OrderByDescending(s => s.points).ToList();

            int playerNumber = 1;
            foreach (Stats player in orderedStats)
            {
                switch (playerNumber)
                {
                    case 1:
                        Player1Username = orderedStats[0].username;
                        Player1Score = orderedStats[0].points;

                        break;
                    case 2:
                        Player2Username = orderedStats[1].username;
                        Player2Score = orderedStats[1].points;

                        break;

                    case 3:
                        Player3Username = orderedStats[2].username;
                        Player3Score = orderedStats[2].points;

                        break;

                    case 4:
                        Player4Username = orderedStats[3].username;
                        Player4Score = orderedStats[3].points;
                        break;

                    default:
                        break;
                }

                playerNumber++;
            }
        }

        public void OnNotifyGameEndedWithoutUsers(int matchCode)
        {
            GameHasEndedWithoutUsers?.Invoke(matchCode);
        }
    }
}