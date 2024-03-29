﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlippiStats.Configuration;
using SlippiStats.Models;
using System;

namespace SlippiStats.Controllers
{
    public class GameController : ApplicationController
    {
        public GameController(ApplicationSettings settings, ApplicationDatabase database) : base(settings, database)
        {

        }

        public IActionResult List()
        {
            GameListViewModel viewModel = new GameListViewModel();

            viewModel.Entries = Game.GetList(Database.Connection);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult List(GameListViewModel viewModel)
        {
            viewModel.Entries = Game.GetListByFilters(Database.Connection, viewModel.PlayerFilter1, viewModel.PlayerFilter2, viewModel.CharacterFilter1, viewModel.CharacterFilter2, viewModel.StageFilter);

            return View(viewModel);
        }

        [Route("{controller}/{id}")]
        public IActionResult Index(int id)
        {
            GameIndexViewModel viewModel = new GameIndexViewModel();
            viewModel.Game = Game.GetById(Database.Connection, id);

            if (viewModel.Game == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            viewModel.ReplayFile = ReplayFile.GetByGameId(Database.Connection, viewModel.Game.Id);

            return View(viewModel);
        }

        public IActionResult DownloadReplay(int gameId)
        {
            Game game = Game.GetById(Database.Connection, gameId);

            if (game == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ReplayFile replayFile = ReplayFile.GetByGameId(Database.Connection, gameId);

            if (replayFile == null)
            {
                return RedirectToAction(nameof(Index), new { id = gameId });
            }

            return File(replayFile.FileData, "application/x-msdownload", String.Format("replay{0}.slp", gameId));
        }

        [AllowAnonymous]
        public IActionResult Upload()
        {
            GameUploadViewModel viewModel = new GameUploadViewModel();

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Upload([FromForm] SlpSettings settings)
        {





            return Json(settings);
        }
    }
}
