using Microsoft.AspNetCore.Mvc;
using SlippiStats.Configuration;

namespace SlippiStats.Controllers
{
    public class TournamentController : ApplicationController
    {
        public TournamentController(ApplicationSettings settings, ApplicationDatabase database)
            : base(settings, database)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            TournamentListViewModel viewModel = new TournamentListViewModel();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}