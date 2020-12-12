using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SlippiStats.Authentication;
using SlippiStats.Configuration;
using SlippiStats.Models;

namespace SlippiStats.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        protected ApplicationController(ApplicationSettings settings, ApplicationDatabase database)
        {
            Settings = settings;
            Database = database;
        }

        protected ApplicationSettings Settings { get; private set; }

        protected ApplicationDatabase Database { get; private set; }

        protected User ApplicationUser { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ApplicationUser = User.GetApplicationUser(Database.Connection);
        }
    }
}