namespace PaydirtPickem.Models
{
    public class UserPick
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PickedTeam { get; set; }
        public Guid GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}