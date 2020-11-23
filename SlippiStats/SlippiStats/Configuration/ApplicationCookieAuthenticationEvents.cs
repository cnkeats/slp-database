using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SlippiStats.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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