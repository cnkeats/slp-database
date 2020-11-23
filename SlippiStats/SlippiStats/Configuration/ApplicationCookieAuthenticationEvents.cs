using Microsoft.AspNetCore.Authentication.Cookies;
using SlippiStats.Configuration;

namespace SlippiStats.Authentication
{
    public class ApplicationCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly ApplicationDatabase database;

        public ApplicationCookieAuthenticationEvents(ApplicationDatabase database)
        {
            this.database = database;
        }
    }
}