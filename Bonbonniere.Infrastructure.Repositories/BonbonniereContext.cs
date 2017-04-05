using Bonbonniere.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Infrastructure.Repositories
{
    //https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/
    public class BonbonniereContext : DbContext
    {
        public BonbonniereContext()
        {

        }

        public BonbonniereContext(DbContextOptions<BonbonniereContext> options) 
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=Bonbonniere;Trusted_Connection=True;");
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
