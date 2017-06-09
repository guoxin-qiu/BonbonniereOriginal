using Bonbonniere.Infrastructure.Environment;
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
                DefaultConnection = "Server=(localdb)\\ProjectsV13;Database=Bonbonniere;Trusted_Connection=True;"
            };
        }

        public Settings Value => _settings;
    }
}
