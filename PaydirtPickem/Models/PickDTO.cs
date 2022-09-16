namespace PaydirtPickem.Models
{
    public class PickDTO
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public double? HomeTeamSpread { get; set; } 
        public DateTime? GameTime { get; set; }
    }
}
