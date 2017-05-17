using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Bonbonniere.Infrastructure.Domain;
using Bonbonniere.Data.Repositories;
using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Infrastructure;
using Bonbonniere.Core.Interfaces;
using System.Linq;
using Bonbonniere.Core.Models;
using System;
using System.Threading.Tasks;
using Bonbonniere.Website.Additions.Conventions;

namespace Bonbonniere.Website
{
    public class Startup
    {
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
            // https://msdn.microsoft.com/en-us/magazine/mt763233.aspx
            // https://github.com/smallprogram/OrganizingAspNetCore
            services.AddMvc(o => o.Conventions.Add(new FeatureConvention()))
                .AddRazorOptions(options =>
                {
                    // {0} - Action Name
                    // {1} - Controller Name
                    // {2} - Area Name
                    // {3} - Feature Name
                    // Replace normal view location entirely
                    options.AreaViewLocationFormats.Clear();
                    options.AreaViewLocationFormats.Add("/Areas/{2}/Features/{3}/{1}/{0}.cshtml");
                    options.AreaViewLocationFormats.Add("/Areas/{2}/Features/{3}/{0}.cshtml");
                    options.AreaViewLocationFormats.Add("/Areas/{2}/Features/Shared/{0}.cshtml");
                    options.AreaViewLocationFormats.Add("/Areas/Shared/{0}.cshtml");

                    // replace normal view location entirely
                    options.ViewLocationFormats.Clear();
                    options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");

                    // add support for features side-by-side with /Views
                    // (do NOT clear ViewLocationFormats)
                    //options.ViewLocationFormats.Insert(0, "/Features/Shared/{0}.cshtml");
                    //options.ViewLocationFormats.Insert(0, "/Features/{3}/{0}.cshtml");
                    //options.ViewLocationFormats.Insert(0, "/Features/{3}/{1}/{0}.cshtml");
                    //
                    // (do NOT clear AreaViewLocationFormats)
                    //options.AreaViewLocationFormats.Insert(0, "/Areas/{2}/Features/Shared/{0}.cshtml");
                    //options.AreaViewLocationFormats.Insert(0, "/Areas/{2}/Features/{3}/{0}.cshtml");
                    //options.AreaViewLocationFormats.Insert(0, "/Areas/{2}/Features/{3}/{1}/{0}.cshtml");

                    options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
                });

            services.Configure<Settings>(Configuration.GetSection("Settings"));
            services.AddScoped<IDataProvider, DataProviderFactory>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBrainstormSessionRepository, BrainstormSessionRepository>();
            services.AddScoped<IMusicStoreRepository, MusicStoreRepository>(); //TODO: mutiply inject?
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IDataProvider dataProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                var brainstormRepo = app.ApplicationServices.GetService<IBrainstormSessionRepository>();
                InitializeDatabaseAsync(brainstormRepo).Wait();
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


        public async Task InitializeDatabaseAsync(IBrainstormSessionRepository repo)
        {
            var sessionList = await repo.ListAsync();
            if (!sessionList.Any())
            {
                await repo.AddAsync(GetTestSession());
            }
        }

        public static BrainstormSession GetTestSession()
        {
            var session = new BrainstormSession
            {
                Name = "Test Session 1",
                DateCreated = new DateTime(2017, 4, 12)
            };
            var idea = new Idea
            {
                DateCreated = new DateTime(2017, 4, 12),
                Description = "Totally awesome idea",
                Name = "Awesome idea"
            };

            session.AddIdea(idea);
            return session;
        }
    }
}
