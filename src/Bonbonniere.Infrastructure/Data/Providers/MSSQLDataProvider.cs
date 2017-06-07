using Bonbonniere.Infrastructure.Environment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bonbonniere.Infrastructure.Data.Providers
{
    public class MSSQLDataProvider : IDataProvider
    {
        private BonbonniereContext _dbContext { get; }

        public BonbonniereContext DbContext => _dbContext;
        public DataProviderType DataProviderType => DataProviderType.MSSQL;

        public MSSQLDataProvider(IOptions<Settings> settings)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BonbonniereContext>();
            optionsBuilder.UseSqlServer(settings.Value.DefaultConnection);

            _dbContext = new BonbonniereContext(optionsBuilder.Options);
        }
    }
}
