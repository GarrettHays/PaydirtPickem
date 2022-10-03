using Microsoft.AspNetCore.Identity;

namespace PaydirtPickem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<League> Leagues { get; set; }   
        public string TeamName { get; set; }

    }
}