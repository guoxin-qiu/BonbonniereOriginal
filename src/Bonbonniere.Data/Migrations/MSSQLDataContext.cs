using Bonbonniere.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Data.Migrations
{
    // Only for database migration
    // Add-Migration *InitializeDatabase* -Context MSSQLDataContext
    // Update-Database -Context MSSQLDataContext
    // Drop-Database -Context MSSQLDataContext
    public class MSSQLDataContext : BonbonniereContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=Bonbonniere;Trusted_Connection=True;");
        }
    }
}
