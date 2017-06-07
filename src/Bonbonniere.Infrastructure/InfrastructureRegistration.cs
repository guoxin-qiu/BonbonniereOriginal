using Bonbonniere.Infrastructure.Data;
using Bonbonniere.Infrastructure.Environment;
using Bonbonniere.Infrastructure.FileSystem;
using Bonbonniere.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Bonbonniere.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static void RegisterInfrastructureModule(this IServiceCollection services)
        {
            services.AddSingleton<IImageService, LocalFileImageService>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IDataProvider, DataProviderFactory>();
            services.AddSingleton<IClock, Clock>();
        }
    }
}
