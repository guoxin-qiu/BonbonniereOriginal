using Bonbonniere.Data.Configurations;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // TODO: how to batch register 
            new AlbumConfiguration(modelBuilder);
            new BrainstormSessionConfiguration(modelBuilder);
            new GenreConfiguration(modelBuilder);
            new IdeaConfiguration(modelBuilder);
            new UserConfiguration(modelBuilder);
            new UserProfileConfiguration(modelBuilder);
        }
    }
}
