namespace PaydirtPickem.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public int? WeekNumber { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public double? HomeTeamSpread { get; set; } 
        public DateTime? GameTime { get; set; }
        public bool IsActive { get; set; }  
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }

    }
}