using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
