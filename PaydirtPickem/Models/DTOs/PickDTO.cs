namespace PaydirtPickem.Models
{
    public class PickDTO
    {
        public int Id { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public double? HomeTeamSpread { get; set; } 
        public DateTime? GameTime { get; set; }
    }
}
