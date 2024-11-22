using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class GameMonster
    {
        public ObservableCollection<GameCard> BodyParts { get; set; } = new ObservableCollection<GameCard>();

        public GameMonster(MonsterDTO monsterDto)
        {
            foreach (var cardDto in monsterDto.bodyParts)
            {
                BodyParts.Add(new GameCard(cardDto));
            }
        }
    }
}
