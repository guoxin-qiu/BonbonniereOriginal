using Bonbonniere.Infrastructure.Data;
using Bonbonniere.Infrastructure.Data.Providers;
using Bonbonniere.Infrastructure.Environment;
using Microsoft.Extensions.Options;
using Moq;

namespace Bonbonniere.UnitTests.DataRepositories.MusicStore
{
    public class RepositoryTestBase
    {
        protected IDataProvider _dataProvider;

        public RepositoryTestBase()
        {
            var mockIOption = new Mock<IOptions<Settings>>();
            _dataProvider = new InMemoryDataProvider(mockIOption.Object);
            BonbonniereContextInitializer.Initialize(_dataProvider.DbContext);
        }
    }
}
