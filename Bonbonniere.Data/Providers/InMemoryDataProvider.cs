using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bonbonniere.Data.Providers
{
    public class InMemoryDataProvider : IDataProvider
    {
        private BonbonniereContext _dbContext { get; }

        public BonbonniereContext DbContext => _dbContext;
        public DataProviderType DataProviderType => DataProviderType.InMemory;

        public InMemoryDataProvider(IOptions<Settings> settings)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BonbonniereContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "Bonbonniere_InMemory");

            _dbContext = new BonbonniereContext(optionsBuilder.Options);
        }
    }
}
