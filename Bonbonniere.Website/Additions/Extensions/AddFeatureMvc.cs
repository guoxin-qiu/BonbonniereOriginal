using Bonbonniere.Website.Additions.Extesions.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Bonbonniere.Website.Additions.Extensions
{
    public static class MvcExtension
    {
        // https://msdn.microsoft.com/en-us/magazine/mt763233.aspx
        // https://github.com/smallprogram/OrganizingAspNetCore

        public static IMvcBuilder AddFeatureMvc(this IServiceCollection services)
        {
            return services.AddMvc(o => o.Conventions.Add(new FeatureConvention()))
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
                    options.ViewLocationFormats.Add("/Features/Shared/Modal/{0}.cshtml");

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
        }
    }
}
