using Microsoft.AspNetCore.Mvc;

namespace SlippiStats.Controllers
{
    public class RequestController : Controller
    {
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
