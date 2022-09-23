namespace PaydirtPickem.Models
{
    public class UserWeekScore
    {
        public Guid Id { get; set; }
        public int WeekNumber { get; set; }
        public Guid UserId { get; set; }
        public int WeekTotalWin { get; set; }
        public int WeekTotalLoss { get; set; }
    }
}