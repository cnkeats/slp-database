using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlippiStats.Configuration;
using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using SlippiStats.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlippiStats.Controllers
{
    public class StatsController : ApplicationController
    {
        public StatsController(ApplicationSettings settings, ApplicationDatabase database) : base(settings, database)
        {

        }

        public IActionResult Index()
        {
            StatsIndexViewModel viewModel = new StatsIndexViewModel();

            List<Game> games = Game.GetList(Database.Connection);

            viewModel.Entries = new List<StatsIndexViewModelEntry>();
            foreach (Game game in games)
            {
                StatsIndexViewModelEntry entry = new StatsIndexViewModelEntry();
                entry.Game = game;
                entry.Player1 = Player.GetById(Database.Connection, (int)game.Player1Id);
                entry.Player2 = Player.GetById(Database.Connection, (int)game.Player2Id);

                viewModel.Entries.Add(entry);
            }

            return View(viewModel);
        }

       [HttpPost]
       public IActionResult Index(StatsIndexViewModel viewModel)
        {
            List<Game> games = Game.GetListByFilters(Database.Connection, viewModel.PlayerFilter1, viewModel.PlayerFilter2, viewModel.CharacterFilter1, viewModel.CharacterFilter2, viewModel.StageFilter);

            viewModel.Entries = new List<StatsIndexViewModelEntry>();
            foreach (Game game in games)
            {
                StatsIndexViewModelEntry entry = new StatsIndexViewModelEntry();
                entry.Game = game;
                entry.Player1 = Player.GetById(Database.Connection, (int)game.Player1Id);
                entry.Player2 = Player.GetById(Database.Connection, (int)game.Player2Id);

                viewModel.Entries.Add(entry);
            }

            return View(viewModel);
        }
    }
}
