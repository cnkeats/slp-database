using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using SlippiStats.Authentication;
using SlippiStats.Configuration;
using SlippiStats.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;

namespace SlippiStats.BusinessLogic
{
    public class AuthenticationBusinessLogic
    {
        private readonly IDbConnection connection;
        private readonly ApplicationSettings settings;

        public AuthenticationBusinessLogic(IDbConnection connection, ApplicationSettings settings)
        {
            this.connection = connection;
            this.settings = settings;
        }

        public string HashPassword(string password)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            return passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            return passwordHasher.VerifyHashedPassword(null, hashedPassword, password) != PasswordVerificationResult.Failed;
        }

        public User GetUserByCredentials(string userName, string password)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            User user = User.GetByUserName(connection, userName);

            if (user == null || user.IsDeleted || passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return user;
        }

        public UserSession GetLatestUserSessionAndExtend(int userId)
        {
            UserSession userSession = UserSession.GetLatestByUserId(connection, userId);

            if (userSession != null && !userSession.IsDeleted && !userSession.IsExpired)
            {
                userSession.Expires = DateTime.UtcNow.AddSeconds(settings.SessionDurationInSeconds);
            }
            else
            {
                userSession = new UserSession(userId, TimeSpan.FromSeconds(settings.SessionDurationInSeconds));
            }

            userSession.Save(connection);

            return userSession;
        }

        public ClaimsPrincipal CreateClaimsPrincipal(UserSession userSession)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ApplicationClaims.SessionId, userSession.Id.ToString())
            };

            List<UserRole> userRoles = UserRole.GetListByUserId(connection, userSession.UserId);

            foreach (UserRole userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.RoleName));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }

        public AuthenticationProperties CreateAuthenticationProperties(UserSession userSession, bool isPersistent)
        {
            return new AuthenticationProperties
            {
                ExpiresUtc = userSession.Expires,
                IsPersistent = isPersistent,
                IssuedUtc = DateTimeOffset.UtcNow
            };
        }
    }
}