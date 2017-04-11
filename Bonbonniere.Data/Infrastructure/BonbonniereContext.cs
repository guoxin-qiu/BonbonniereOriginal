using Bonbonniere.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Data.Infrastructure
{
    public class BonbonniereContext : DbContext
    {
        public BonbonniereContext()
        {
        }

        public BonbonniereContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BrainstormSession> BrainStormSessions { get; set; }
    }
}
