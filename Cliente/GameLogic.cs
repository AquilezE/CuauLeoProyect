using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{


    public class GameLogic
    {

        

        //private GameStateDTO currentState;

        public int GameId { get; set; }

        //public ObservableCollection<CardDTO> BabyDeck { get; set; }

        //public ObservableCollection<CardDTO> Hand { get; set; }

        //public ObservableCollection<MonsterDTO> Monster { get; set; }

        public int CurrentPlayerId { get; set; }

        public int ActionsRemaining { get; set; }






        //casteador de gameDTO a gameLogic debe incluir el casteador de cardDTO a carta con fotito
        //esto incluye meterse al playerState y castear las cartas dentro de la mano y los monstros  

    }
}
