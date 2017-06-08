using Bonbonniere.Infrastructure;
using Bonbonniere.Infrastructure.Data;
using Bonbonniere.Infrastructure.Environment;
using Bonbonniere.Services;
using Bonbonniere.Website.Additions.Extensions;
using Bonbonniere.Website.Additions.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;

namespace Bonbonniere.Website
{
    public class Startup
    {
        private IServiceCollection _services;
        private IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            _hostingEnvironment = env;

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
            services.AddFeatureMvc(_hostingEnvironment);

            services.Configure<Settings>(Configuration.GetSection("Settings"));

            services.RegisterInfrastructureModule();
            services.RegisterServiceModule();

            services.AddHttpContextAccessor();
            services.AddDirectoryBrowser();

            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
            AppsContext bonbonniereContext, SampleContext sampleContext, EnglishClassContext englishClassContext)
        {
            app.UseRequestCulture();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                LoginPath = new PathString("/Account/SignIn"),
                AccessDeniedPath = new PathString("/Account/Forbidden"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            loggerFactory.WithFilter(new FilterLoggerSettings
            {
                { "Microsoft", LogLevel.Warning },
                { "System", LogLevel.Warning },
                { "Bonbonniere", LogLevel.Debug }
            })
            .AddConsole(Configuration.GetSection("Logging"))
            .AddDebug();

            if (env.IsDevelopment() || env.IsEnvironment("Test"))
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseAllServicesMap(_services);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Serve my app-specific default file, if present.
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("mydefault.html");
            app.UseDefaultFiles(options); // must be called before 'UseStaticFiles'

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=800");
                }
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(_hostingEnvironment.ContentRootPath, @"MyStaticFiles")),
                RequestPath = new PathString("/StaticFiles"),
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
                }
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(_hostingEnvironment.ContentRootPath, @"wwwroot", "images")),
                RequestPath = new PathString("/MyImages")
            });

            app.UseStaticHttpContext();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            AppsContextInitializer.Initialize(bonbonniereContext);
            SampleContextInitializer.Initialize(sampleContext);
            EnglishClassContextInitializer.Initialize(englishClassContext);
        }
    }
}
