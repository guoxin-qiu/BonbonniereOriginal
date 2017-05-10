using Bonbonniere.Infrastructure;
using Microsoft.Extensions.Options;

namespace Bonbonniere.IntegrationTests
{
    public class TestAppSettings : IOptions<Settings>
    {
        private readonly Settings _settings;

        public TestAppSettings(bool storeInCache = true)
        {
            _settings = new Settings()
            {
                DefaultConnection = string.Empty,
                DataProvider = DataProviderType.InMemory
            };
        }

        public Settings Value => _settings;
    }
}
