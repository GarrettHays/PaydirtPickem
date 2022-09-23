namespace PaydirtPickem.Models
{
    public class UserPick
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PickedTeam { get; set; }
        public string GameId { get; set; }
    }
}