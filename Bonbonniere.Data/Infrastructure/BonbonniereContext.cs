using Bonbonniere.Core.Models;
using Bonbonniere.Core.Models.MusicStore;
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
        public DbSet<Album> Albums { get; set; }
    }
}
