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
            viewModel.Games = Game.GetList(Database.Connection);
            return View(viewModel);
        }

       [HttpPost]
       public IActionResult Index(StatsIndexViewModel viewModel)
        {
            viewModel.Games = Game.GetListByFilters(Database.Connection, viewModel.PlayerFilter1, viewModel.PlayerFilter2, viewModel.CharacterFilter1, viewModel.CharacterFilter2, viewModel.StageFilter);
            return View(viewModel);
        }
    }
}
