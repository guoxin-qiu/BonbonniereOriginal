using Microsoft.Extensions.DependencyInjection;

namespace Bonbonniere.Services
{
    public static class ServiceRegistation
    {
        public static void RegisterServiceModule(this IServiceCollection services)
        {
            // TODO: mutiply inject?
            services.AddScoped<IBrainstormService, BrainstormService>();
            services.AddScoped<IMusicStoreService, MusicStoreService>(); 
            services.AddScoped<IUserService, UserService>();
        }
    }
}
