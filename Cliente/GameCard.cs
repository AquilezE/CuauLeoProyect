using Cliente.ServiceReference;

namespace Cliente
{
    public class GameCard
    {
        public int CardId { get; set; }
        public string CardPath { get; set; }

        public GameCard(string cardPath)
        {
            CardPath = cardPath;
        }

        public GameCard(CardDTO cardDto)
        {
            CardId = cardDto.CardId;
            CardPath = "pack://application:,,,/Cards/Card" + cardDto.CardId + ".png";
        }
    }
}