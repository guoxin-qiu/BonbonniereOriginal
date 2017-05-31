using Bonbonniere.Data;
using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Infrastructure;
using Bonbonniere.Services;
using Bonbonniere.Website.Additions.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Xunit.Abstractions;

namespace Bonbonniere.IntegrationTests
{
    public class IntegrationTestBase
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ITestOutputHelper _output;
        protected readonly BonbonniereContext _dbContext;

        public IntegrationTestBase()
        {
            var services = new ServiceCollection();
            //services.AddOptions();
            services.AddSingleton(typeof(IOptions<Settings>), typeof(TestAppSettings));
            services.AddSingleton(typeof(ILogger<>), typeof(TestLogger<>));

            services.RegisterRepositoryModule();
            services.RegisterServiceModule();

            services.AddScoped(typeof(GenreMenuViewComponent));

            _serviceProvider = services.BuildServiceProvider();

            _dbContext = _serviceProvider.GetRequiredService<IDataProvider>().DbContext;
        }

        public IntegrationTestBase(ITestOutputHelper output) : this()
        {
            _output = output;
        }
    }
}
