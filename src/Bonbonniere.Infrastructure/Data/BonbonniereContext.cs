using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Infrastructure.Data
{
    public class BonbonniereContext : DbContext
    {
        public BonbonniereContext(DbContextOptions<BonbonniereContext> options) : base(options)
        {

        }
    }
}
