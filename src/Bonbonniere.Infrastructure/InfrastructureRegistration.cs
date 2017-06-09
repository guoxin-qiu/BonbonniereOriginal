using Bonbonniere.Infrastructure.Data;
using Bonbonniere.Infrastructure.Environment;
using Bonbonniere.Infrastructure.FileSystem;
using Bonbonniere.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bonbonniere.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static void RegisterInfrastructureModule(this IServiceCollection services, Settings settings)
        {
            var conStr = settings.DefaultConnection;

            services.AddSingleton<IImageService, LocalFileImageService>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddSingleton<IClock, Clock>();

            services.AddDbContext<BonbonniereContext>(DbOptions(conStr));
            services.AddDbContext<AppsContext>(DbOptions(conStr));
            services.AddDbContext<SampleContext>(DbOptions(conStr));
            services.AddDbContext<EnglishClassContext>(DbOptions(conStr));
        }

        private static Action<DbContextOptionsBuilder> DbOptions(string connectionString)
        {
            return options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Bonbonniere.Data"));
                //options.UseInMemoryDatabase();
            };
        }
    }
}
