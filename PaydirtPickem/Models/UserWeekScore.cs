namespace PaydirtPickem.Models
{
    public class UserWeekScore
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public int UserId { get; set; }
        public int WeekTotalWin { get; set; }
        public int WeekTotalLoss { get; set; }
    }
}