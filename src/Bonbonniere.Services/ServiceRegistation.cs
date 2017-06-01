using Bonbonniere.Services.Implementations;
using Bonbonniere.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;

namespace Bonbonniere.Services
{
    public static class ServiceRegistation
    {
        public static void RegisterServiceModule(this IServiceCollection services)
        {
            // Reference: https://github.com/JeremySkinner/FluentValidation/blob/master/src/FluentValidation.AspNetCore/FluentValidationMvcExtensions.cs

            var assembly = typeof(ServiceRegistation).GetTypeInfo().Assembly;

            var query = (from type in assembly.GetExportedTypes().Where(t => t.Name.EndsWith("Service"))
                         let matchingInterface = type.GetInterfaces().FirstOrDefault(t => t.Name.EndsWith("Service"))
                         where matchingInterface != null
                         select new { matchingInterface, type }).ToList();

            foreach (var pair in query)
            {
                services.AddScoped(pair.matchingInterface, pair.type);
            }
        }
    }
}
