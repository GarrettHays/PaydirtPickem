namespace PaydirtPickem.Models
{
    public class UserSeasonScore
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SeasonTotalWin { get; set; }
        public int SeasonTotalLoss { get; set; }
    }
}