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
        private static readonly object _lock = new object();

        // Singleton Instance
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

        public ObservableCollection<CardDTO> BabyDeck { get; set; } = new ObservableCollection<CardDTO>();
        public ObservableCollection<CardDTO> Hand { get; set; } = new ObservableCollection<CardDTO>();
        public ObservableCollection<MonsterDTO> Monster { get; set; } = new ObservableCollection<MonsterDTO>();

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
                BabyDeck.Add(card);
            }


            var currentPlayerState = gameState.playerState
                .FirstOrDefault(ps => ps.Key == CurrentPlayerId).Value;
           

                    var newCard = gameState.playerState.FirstOrDefault(ps => ps.Key == User.Instance.ID).Value.Hand.Last(); ;
                    LastCardDrawn = newCard != null ? $"Card ID: {newCard.CardId}" : "No card drawn";
                Console.WriteLine(LastCardDrawn);




            Hand.Clear();
                foreach (var card in currentPlayerState.Hand)
                {
                    Hand.Add(card);
                }


                Monster.Clear();
                foreach (var monster in currentPlayerState.Monsters)
                {
                    Monster.Add(monster);
                }

            }
        }
    }


