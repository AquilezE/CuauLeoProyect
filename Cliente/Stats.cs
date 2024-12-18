using Cliente.ServiceReference;

namespace Cliente
{

    public class Stats
    {

        public string Username { get; set; }
        public int Points { get; set; }

        public Stats(string username, int points)
        {
            this.Username = username;
            this.Points = points;
        }

        public Stats(StatsDTO statsDto)
        {
            Username = statsDto.Username;
            Points = statsDto.PointsThisGame;
        }

    }

}