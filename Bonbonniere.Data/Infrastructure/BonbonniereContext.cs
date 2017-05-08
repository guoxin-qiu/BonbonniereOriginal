using Bonbonniere.Core.Models;
using Bonbonniere.Core.Models.MusicStore;
using Bonbonniere.Core.Models.WordTutor;
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
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Root> WordRoots { get; set; }
        public DbSet<Suffix> WordSuffixes { get; set; }
    }
}
