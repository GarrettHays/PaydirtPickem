using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaydirtPickem.Models;

namespace PaydirtPickem.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
            public DbSet<Game> Games { get; set; }
            public DbSet<UserPick> UserPicks { get; set; }
            public DbSet<UserSeasonScore> UserSeasonScores { get; set; }
            public DbSet<UserWeekScore> UserWeekScores { get; set; }
        }
    }
}