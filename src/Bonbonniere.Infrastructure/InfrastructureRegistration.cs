using Bonbonniere.Infrastructure.Data;
using Bonbonniere.Infrastructure.Environment;
using Bonbonniere.Infrastructure.FileSystem;
using Bonbonniere.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Infrastructure
{
    public static class InfrastructureRegistration
    {
        // TODO: how to get the dbUrl from config.json?
        public static string dbUrl = "Server=(localdb)\\ProjectsV13;Database=Bonbonniere;Trusted_Connection=True;";
        public static void RegisterInfrastructureModule(this IServiceCollection services)
        {
            services.AddSingleton<IImageService, LocalFileImageService>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddSingleton<IClock, Clock>();

            services.AddDbContext<BonbonniereContext>(options =>
            {
                //options.UseSqlServer(dbUrl, b => b.MigrationsAssembly("Bonbonniere.Data"));
                options.UseInMemoryDatabase("Db_Bonbonniere");
            });

            services.AddDbContext<AppsContext>(options =>
            {
                //options.UseSqlServer(dbUrl, b => b.MigrationsAssembly("Bonbonniere.Data"));
                options.UseInMemoryDatabase("Db_Bonbonniere");
            });
            services.AddDbContext<SampleContext>(options =>
            {
                //options.UseSqlServer(dbUrl, b => b.MigrationsAssembly("Bonbonniere.Data"));
                options.UseInMemoryDatabase("Db_Sample");
            });
            services.AddDbContext<EnglishClassContext>(options =>
            {
                //options.UseSqlServer(dbUrl, b => b.MigrationsAssembly("Bonbonniere.Data"));
                options.UseInMemoryDatabase("Db_EnglishClass");
            });
        }
    }
}
