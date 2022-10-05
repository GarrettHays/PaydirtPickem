namespace PaydirtPickem.Models.DTOs
{
    public class SeasonScoreDTO
    {
        public Guid UserId { get; set; }
        public int SeasonTotalWin { get; set; }
        public int SeasonTotalLoss { get; set; }
        public string TeamName { get; set; }
    }
}
