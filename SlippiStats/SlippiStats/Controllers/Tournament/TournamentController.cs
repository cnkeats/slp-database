using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlippiStats.Configuration;
using SlippiStats.Models;
using System;
using System.Collections.Generic;

namespace SlippiStats.Controllers
{
    [AllowAnonymous]
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
        public IActionResult Upload(string tournamentName)
        {
            //List<IFormFile> files = viewModel.Files;
            return View();
        }

        [HttpGet]
        public string Check(string tournamentName)
        {
            Tournament tournament = Tournament.GetByName(Database.Connection, tournamentName);

            return tournament != null ? JsonConvert.SerializeObject(tournament) : null;
        }

        [HttpPost]
        public Tournament Create(string tournamentName, DateTime startDate, DateTime? endDate)
        {
            Tournament tournament = Tournament.GetByName(Database.Connection, tournamentName);

            if (tournament != null)
            {
                // Shouldn't happen
                return tournament;
            }

            tournament = new Tournament();
            tournament.Name = tournamentName;
            tournament.StartDate = startDate;
            tournament.EndDate = endDate;
            tournament.Save(Database.Connection);

            return tournament;
        }

        [AllowAnonymous]
        [HttpPost]
        public Game UploadGame(Game game, int tournamentId)
        {
            game.Save(Database.Connection);
            game.AssignToTournament(Database.Connection, tournamentId);

            return game;
        }
    }
}