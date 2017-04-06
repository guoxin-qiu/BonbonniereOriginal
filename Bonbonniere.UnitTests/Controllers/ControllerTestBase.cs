using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Data.Repositories;
using Bonbonniere.Infrastructure;
using Bonbonniere.Infrastructure.Domain;
using Bonbonniere.Website.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bonbonniere.UnitTests.Controllers
{
    public class ControllerTestBase
    {
        private IServiceProvider _serviceProvider;

        protected ControllerTestBase()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var _service = new ServiceCollection();
            _service.Configure<Settings>(configuration.GetSection("Settings"));
            _service.AddOptions();
            _service.AddScoped<IDataProvider, DataProviderFactory>();
            _service.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            _service.AddScoped<IUnitOfWork, UnitOfWork>();

            _service.AddScoped<AccountController>(); //TODO: can multiple inject?

            _serviceProvider = _service.BuildServiceProvider();
        }

        protected T GetInstance<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
