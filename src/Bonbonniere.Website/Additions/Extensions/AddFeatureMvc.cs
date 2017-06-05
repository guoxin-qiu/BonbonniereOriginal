using Bonbonniere.Website.Additions.Extesions.Conventions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Bonbonniere.Website.Additions.Extensions
{
    public static class MvcExtension
    {
        // https://msdn.microsoft.com/en-us/magazine/mt763233.aspx
        // https://github.com/smallprogram/OrganizingAspNetCore
        // https://github.com/blowdart/AspNetAuthorizationWorkshop Step 2

        public static IMvcBuilder AddFeatureMvc(this IServiceCollection services, IHostingEnvironment env)
        {
            return services.AddMvc(config =>
            {
                config.Conventions.Add(new FeatureConvention());
                if (!env.IsEnvironment("FunctionalTest"))
                {
                    config.Filters.Add(new AuthorizeFilter(
                        new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
                }
            }).AddJsonOptions(options=> 
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
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
                options.ViewLocationFormats.Add("/Features/{3}/{1}.{0}.cshtml");
                options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
                options.ViewLocationFormats.Add("/Features/Shared/Modal/{0}.cshtml");

                options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
            });
        }
    }
}
