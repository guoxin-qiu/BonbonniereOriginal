using Bonbonniere.Infrastructure;
using Bonbonniere.Infrastructure.Data;
using Bonbonniere.Infrastructure.Environment;
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
        protected readonly AppsContext _appsContext;
        protected readonly SampleContext _sampleContext;
        protected readonly EnglishClassContext _englishClassContext;

        public IntegrationTestBase()
        {
            var services = new ServiceCollection();
            //services.AddOptions();
            services.AddSingleton(typeof(IOptions<Settings>), typeof(TestAppSettings));

            services.RegisterInfrastructureModule();
            services.AddSingleton(typeof(ILogger<>), typeof(TestLogger<>));

            services.RegisterServiceModule();

            services.AddScoped(typeof(GenreMenuViewComponent));

            _serviceProvider = services.BuildServiceProvider();
            _appsContext = _serviceProvider.GetRequiredService<Bonbonniere.Infrastructure.Data.AppsContext>();
            _sampleContext = _serviceProvider.GetRequiredService<SampleContext>();
            _englishClassContext = _serviceProvider.GetRequiredService<EnglishClassContext>();
        }

        public IntegrationTestBase(ITestOutputHelper output) : this()
        {
            _output = output;
        }
    }
}
