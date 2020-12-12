using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SlippiStats.Authentication;
using SlippiStats.Configuration;
using System.Diagnostics;

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
            //return RedirectToAction(nameof(GameController.Index), "Game");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}