using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlippiStats.Configuration;
using SlippiStats.Models;
using System.Collections.Generic;

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
            viewModel.Entries = Tournament.GetList(Database.Connection);

            return View(viewModel);
        }

        public IActionResult Upload()
        {
            TournamentListViewModel viewModel = new TournamentListViewModel();

            return View();
        }

        [HttpPost]
        public IActionResult Upload(TournamentUploadViewModel viewModel)
        {
            //List<IFormFile> files = viewModel.Files;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public string Check(string tournamentName)
        {
            Tournament tournament = Tournament.GetByName(Database.Connection, tournamentName);

            return tournament != null ? JsonConvert.SerializeObject(tournament) : null;
        }
    }
}