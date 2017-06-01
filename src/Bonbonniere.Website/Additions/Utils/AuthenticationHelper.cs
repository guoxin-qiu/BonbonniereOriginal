using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Bonbonniere.Website.Additions.Utils
{
    public static class AuthenticationHelper
    {
        public static void SetAuthentication(this HttpContext httpContext,
            string email, string fullName, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, email, ClaimValueTypes.String, null),
                new Claim(ClaimTypes.GivenName, fullName, ClaimValueTypes.String, null)
            };
            var userIdentity = new ClaimsIdentity("SuperSecureLogin");
            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            httpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = isPersistent,
                    AllowRefresh = false
                }).Wait();
        }

        public static void RemoveAuthentication(this HttpContext httpContext)
        {
            httpContext.Authentication.SignOutAsync("Cookie").Wait();
        }
    }
}
