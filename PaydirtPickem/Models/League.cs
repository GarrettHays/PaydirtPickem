namespace PaydirtPickem.Models
{
    public class League
    {
        public Guid Id { get; set; }
        public string LeagueName { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
