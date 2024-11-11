using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class Card
    {
        public string CardPath { get; set; }

        public Card(string cardPath) 
        {
            CardPath = cardPath;
        }

        public Card(CardDTO cardDto)
        {
            CardPath = "pack://application:,,,/Cards/Card" + cardDto.CardId + ".png";
        }
    }
}
