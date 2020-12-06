using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SlippiStats.Configuration;
using SlippiStats.Models;
using SlippiStats.Models.ReplaySubmission;
using SlippiStats.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SlippiStats.Controllers
{
    public class GameController : ApplicationController
    {
        public GameController(ApplicationSettings settings, ApplicationDatabase database) : base(settings, database)
        {

        }

        public IActionResult Index(int id)
        {
            GameIndexViewModel viewModel = new GameIndexViewModel();
            viewModel.Game = Game.GetById(Database.Connection, id);

            if (viewModel.Game != null)
            {
                viewModel.ReplayFile = ReplayFile.GetByGameIdUploaderId(Database.Connection, viewModel.Game.Id, 0);
            }

            return View(viewModel);
        }

        public IActionResult DownloadReplay(int gameId)
        {
            Game game = Game.GetById(Database.Connection, gameId);

            if (game == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ReplayFile replayFile = ReplayFile.GetByGameIdUploaderId(Database.Connection, game.Id, 0);

            if (replayFile == null)
            {
                return RedirectToAction(nameof(Index), new { id = gameId });
            }

            return File(replayFile.FileData, "application/x-msdownload", String.Format("replay{0}.slp", gameId));
        }
    }
}