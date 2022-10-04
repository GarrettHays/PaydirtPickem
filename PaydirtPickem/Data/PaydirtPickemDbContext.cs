using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaydirtPickem.Models;

namespace PaydirtPickem.Data
{
    public class PaydirtPickemDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public PaydirtPickemDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserPick> UserPicks { get; set; }
        public DbSet<UserSeasonScore> UserSeasonScores { get; set; }
        public DbSet<UserWeekScore> UserWeekScores { get; set; }
        public DbSet<League> League { get; set; }
        public DbSet<LastDateScored> LastScored { get; set; }

    }
}