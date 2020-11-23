using Microsoft.AspNetCore.Mvc;
using SlippiStats.Configuration;

namespace SlippiStats.Controllers
{
    public class RequestController : ApplicationController
    {
        public RequestController(ApplicationSettings settings, ApplicationDatabase database) : base(settings, database)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MetaStats()
        {
            return View();
        }
    }
}
