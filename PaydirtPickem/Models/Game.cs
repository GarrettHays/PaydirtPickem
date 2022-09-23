namespace PaydirtPickem.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public int HomeTeam { get; set; }
        public int AwayTeam { get; set; }
        public double? HomeTeamSpread { get; set; } 
        public DateTime? GameTime { get; set; }
    }
}