using Microsoft.AspNetCore.Mvc;
using SlippiStats.Configuration;

namespace SlippiStats.Controllers
{
    public class HomeController : ApplicationController
    {
        public HomeController(ApplicationSettings settings, ApplicationDatabase database)
            : base(settings, database)
        {

        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(GameController.Upload), "Game");
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}