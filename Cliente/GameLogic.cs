using Cliente.ServiceReference;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{


    public class GameLogic : INotifyPropertyChanged, IGameManagerCallback
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private static GameLogic _instance;

        public List<int> playerMapping = new List<int>();



        private static readonly object _lock = new object();

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




        private GameStateDTO _currentGameState;
        public int GameId { get; set; }

        //public ObservableCollection<GameCard> Player1Hand { get; set; } = new ObservableCollection<GameCard>();
        //public ObservableCollection<GameCard> Player2Hand { get; set; } = new ObservableCollection<GameCard>();
        //public ObservableCollection<GameCard> Player3Hand { get; set; } = new ObservableCollection<GameCard>();
        //public ObservableCollection<GameCard> Player4Hand { get; set; } = new ObservableCollection<GameCard>();

        public ObservableCollection<GameCard> BabyDeck { get; set; } = new ObservableCollection<GameCard>();
        public ObservableCollection<CardDTO> Hand { get; set; } = new ObservableCollection<CardDTO>();
        public ObservableCollection<MonsterDTO> Monster { get; set; } = new ObservableCollection<MonsterDTO>();
        public ObservableCollection<GameCard> CardListViewer { get; set; } = new ObservableCollection<GameCard>();


        private string _lastCardDrawn;

        public string LastCardDrawn
        {
            get => _lastCardDrawn;
            set
            {
                _lastCardDrawn = value;
                OnPropertyChanged(nameof(LastCardDrawn));
            }
        }

        private int _currentPlayerId;

        public int CurrentPlayerId
        {
            get => _currentPlayerId;
            set
            {
                _currentPlayerId = value;
                OnPropertyChanged(nameof(CurrentPlayerId));
            }
        }


        private int _actionsRemaining;

        public int ActionsRemaining
        {
            get => _actionsRemaining;
            set
            {
                _actionsRemaining = value;
                OnPropertyChanged(nameof(ActionsRemaining));
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

        //public void ConfigureAllPlayerHands()
        //{

        //    Player1Hand.Clear();
        //    Player2Hand.Clear();
        //    Player3Hand.Clear();
        //    Player4Hand.Clear();

        //    foreach (var player in CurrentGameState.playerState)
        //    {
        //        var hand = player.Value.Hand;
        //        var playerHand = new ObservableCollection<GameCard>();

        //        foreach (var card in hand)
        //        {
        //            playerHand.Add(new GameCard(card));
        //        }

        //        if (player.Key == User.Instance.ID)
        //        {
        //            Hand = playerHand;
        //        }
        //        else if (playerMapping.Count > 0 && player.Key == playerMapping[0])
        //        {
        //            Player1Hand = playerHand;
        //        }
        //        else if (playerMapping.Count > 1 && player.Key == playerMapping[1])
        //        {
        //            Player2Hand = playerHand;
        //        }
        //        else if (playerMapping.Count > 2 && player.Key == playerMapping[2])
        //        {
        //            Player3Hand = playerHand;
        //        }
        //        else if (playerMapping.Count > 3 && player.Key == playerMapping[3])
        //        {
        //            Player4Hand = playerHand;
        //        }
        //    }






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

            MapPlayers();


            //ConfigureAllPlayerHands();

        }


        private void MapPlayers()
        {
            foreach (var player in CurrentGameState.playerState)
            {
                if (player.Value.User.UserId != User.Instance.ID)
                {
                    playerMapping.Add(player.Value.User.UserId);
                }
            }
        }


        private void UpdatePropertiesFromGameState(GameStateDTO gameState)
        {
            Console.WriteLine("Estoy cambiando??");
            if (gameState == null)
                return;

            GameId = gameState.GameStateId;
            CurrentPlayerId = gameState.CurrentPlayerId;
            ActionsRemaining = gameState.ActionsRemaining;

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

            foreach (var card in CurrentGameState.playerState[User.Instance.ID].Hand)
            {
                CardListViewer.Add(new GameCard(card));
            }


        }
    }
}


