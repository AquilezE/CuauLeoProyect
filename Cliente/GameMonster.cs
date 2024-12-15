using Cliente.ServiceReference;
using System.Collections.ObjectModel;

namespace Cliente
{
    public class GameMonster
    {
        public ObservableCollection<GameCard> BodyParts { get; set; } = new ObservableCollection<GameCard>();

        public GameMonster(MonsterDTO monsterDto)
        {
            foreach (CardDTO cardDto in monsterDto.BodyParts) BodyParts.Add(new GameCard(cardDto));
        }
    }
}