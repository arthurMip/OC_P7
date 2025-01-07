using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dot.Net.WebApi.Data
{
    public class LocalDbContext : IdentityDbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<BidList> BidLists { get; set; }
        public DbSet<RuleName> RuleNames { get; set; }
        public DbSet<CurvePoint> CurvePoints { get; set; }
    }
}