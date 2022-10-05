using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaydirtPickem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        private string teamName = "";
        public virtual ICollection<League> Leagues { get; set; }   
        public string TeamName
        {
            get
            {
                return teamName;
            }
            set
            {
                teamName = value;
            }
        }
    }
}