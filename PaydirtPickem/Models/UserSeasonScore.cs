namespace PaydirtPickem.Models
{
    public class UserSeasonScore
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int SeasonTotalWin { get; set; }
        public int SeasonTotalLoss { get; set; }
    }
}