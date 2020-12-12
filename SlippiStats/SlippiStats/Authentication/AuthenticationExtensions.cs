using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using SlippiStats.Models;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace SlippiStats.Authentication
{
    public static class AuthenticationExtensions
    {
        public static void AddApplicationAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.EventsType = typeof(ApplicationCookieAuthenticationEvents);
                });

            services.AddScoped<ApplicationCookieAuthenticationEvents>();
        }

        public static User GetApplicationUser(this ClaimsPrincipal principal, IDbConnection connection)
        {
            Claim sessionIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ApplicationClaims.SessionId);

            if (sessionIdClaim == null)
            {
                return null;
            }

            Guid sessionId = new Guid(sessionIdClaim.Value);
            UserSession userSession = UserSession.GetById(connection, sessionId);

            return User.GetById(connection, userSession.UserId);
        }
    }
}