using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Infrastructure.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Bonbonniere.Data
{
    public static class RepositoryRegistration
    {
        public static void RegisterRepositoryModule(this IServiceCollection services)
        {
            services.AddScoped<IDataProvider, DataProviderFactory>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IReadonlyRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
