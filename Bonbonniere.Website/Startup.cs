using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Infrastructure;
using Bonbonniere.Services;
using Bonbonniere.Website.Additions.Extensions;
using Bonbonniere.Data;
using Bonbonniere.Website.Additions.Middleware;
using Bonbonniere.Infrastructure.FileSystem;
using Bonbonniere.Infrastructure.Logging;
using Microsoft.AspNetCore.Http;

namespace Bonbonniere.Website
{
    public class Startup
    {
        private IServiceCollection _services;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFeatureMvc();

            services.Configure<Settings>(Configuration.GetSection("Settings"));

            services.AddSingleton<IImageService, LocalFileImageService>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.RegisterRepositoryModule();
            services.RegisterServiceModule();

            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IDataProvider dataProvider)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookie",
                LoginPath = new PathString("/Account/SignIn"),
                AccessDeniedPath = new PathString("/Account/Forbidden"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseAllServicesMap(_services);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            BonbonniereContextInitializer.Initialize(dataProvider.DbContext);
        }
    }
}
