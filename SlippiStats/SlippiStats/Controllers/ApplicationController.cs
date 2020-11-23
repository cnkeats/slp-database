using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SlippiStats.Configuration;

namespace SlippiStats.Controllers
{
    public class ApplicationController : Controller
    {
        protected ApplicationController(ApplicationSettings settings, ApplicationDatabase database)
        {
            Settings = settings;
            Database = database;
        }

        protected ApplicationSettings Settings { get; private set; }

        protected ApplicationDatabase Database { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}