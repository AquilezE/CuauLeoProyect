using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class Stats
    {
        public string username{ get; set; }
        public int points { get; set; }

        public Stats(string username, int points)
        {
            this.username = username;
            this.points = points;
        }

        public Stats(StatsDTO statsDto)
        {
            username = statsDto.Username;
            points = statsDto.PointsThisGame;
        }
    }
}
