using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
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
    }
}