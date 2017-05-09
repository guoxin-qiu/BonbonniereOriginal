using Bonbonniere.Core.Interfaces;
using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Data.Repositories;
using Bonbonniere.Infrastructure;
using Bonbonniere.Infrastructure.Domain;
using Bonbonniere.Website.Components;
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
            services.AddSingleton(typeof(IOptions<Settings>), GetMockedOptions());
            services.AddSingleton(typeof(ILogger<>), typeof(MockedLogger<>));
            services.AddScoped<IDataProvider, DataProviderFactory>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBrainstormSessionRepository, BrainstormSessionRepository>();
            services.AddScoped<IMusicStoreRepository, MusicStoreRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(GenreMenuViewComponent));

            _serviceProvider = services.BuildServiceProvider();

            _dbContext = _serviceProvider.GetRequiredService<IDataProvider>().DbContext;
        }

        public IntegrationTestBase(ITestOutputHelper output) : this()
        {
            _output = output;
        }

        private IOptions<Settings> GetMockedOptions()
        {
            var options = Options.Create(
                new Settings
                {
                    DefaultConnection = string.Empty,
                    DataProvider = DataProviderType.InMemory
                }
            );

            return options;
        }
    }

    public class MockedLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            
        }
    }
}
