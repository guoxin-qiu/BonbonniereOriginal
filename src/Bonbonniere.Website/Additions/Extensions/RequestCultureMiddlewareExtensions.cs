using Bonbonniere.Website.Additions.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Bonbonniere.Website.Additions.Extensions
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder build)
        {
            return build.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
