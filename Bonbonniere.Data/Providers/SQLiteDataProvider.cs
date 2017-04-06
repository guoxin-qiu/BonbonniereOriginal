using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bonbonniere.Data.Providers
{
    public class SQLiteDataProvider : IDataProvider
    {
        private BonbonniereContext _dbContext { get; }

        public BonbonniereContext DbContext => _dbContext;
        public DataProviderType DataProviderType => DataProviderType.SQLite;

        public SQLiteDataProvider(IOptions<Settings> settings)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BonbonniereContext>();
            optionsBuilder.UseSqlite(settings.Value.DefaultConnection);

            _dbContext = new BonbonniereContext(optionsBuilder.Options);
        }
    }
}
