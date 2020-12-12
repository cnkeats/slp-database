using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SlippiStats.Configuration;
using SlippiStats.Models;
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

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            Guid sessionId = new Guid(context.Principal.Claims.First(c => c.Type == ApplicationClaims.SessionId).Value);
            UserSession userSession = UserSession.GetById(database.Connection, sessionId);

            if (userSession == null || userSession.IsDeleted || userSession.IsExpired)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return;
            }

            User user = User.GetById(database.Connection, userSession.UserId);

            if (user == null || user.IsDeleted)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            userSession.Expires = DateTime.UtcNow.AddDays(1);
            userSession.Save(database.Connection);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ApplicationClaims.SessionId, userSession.Id.ToString())
            };

            List<UserRole> userRoles = UserRole.GetListByUserId(database.Connection, user.Id);

            foreach (UserRole userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.RoleName));
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            context.ReplacePrincipal(claimsPrincipal);
            context.Properties.ExpiresUtc = userSession.Expires;
            context.ShouldRenew = true;
        }
    }
}