using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

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