using Bonbonniere.Infrastructure.Data.Providers;
using Bonbonniere.Infrastructure.Environment;
using Microsoft.Extensions.Options;

namespace Bonbonniere.Infrastructure.Data
{
    public class DataProviderFactory : IDataProvider
    {
        private IDataProvider _dataProvider { get; }

        public BonbonniereContext DbContext => _dataProvider.DbContext;

        public DataProviderType DataProviderType => _dataProvider.DataProviderType;

        public DataProviderFactory(IOptions<Settings> settings)
        {
            switch (settings.Value.DataProvider)
            {
                case DataProviderType.InMemory:
                    _dataProvider = new InMemoryDataProvider(settings);
                    break;
                case DataProviderType.MSSQL:
                    _dataProvider = new MSSQLDataProvider(settings);
                    break;
                default:
                    _dataProvider = new InMemoryDataProvider(settings);
                    break;
            }
        }
    }
}
