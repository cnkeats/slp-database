using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlippiStats.Configuration;
using SlippiStats.Models;
using System;
using System.Collections.Generic;

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

            List<Game> games = Game.GetList(Database.Connection);

            viewModel.Entries = new List<GameListEntry>();
            foreach (Game game in games)
            {
                GameListEntry entry = new GameListEntry();
                entry.Game = game;
                entry.Player1 = Player.GetById(Database.Connection, (int)game.Player1Id);
                entry.Player2 = Player.GetById(Database.Connection, (int)game.Player2Id);

                viewModel.Entries.Add(entry);
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult List(GameListViewModel viewModel)
        {
            List<Game> games = Game.GetListByFilters(Database.Connection, viewModel.PlayerFilter1, viewModel.PlayerFilter2, viewModel.CharacterFilter1, viewModel.CharacterFilter2, viewModel.StageFilter);

            viewModel.Entries = new List<GameListEntry>();
            foreach (Game game in games)
            {
                GameListEntry entry = new GameListEntry();
                entry.Game = game;
                entry.Player1 = Player.GetById(Database.Connection, (int)game.Player1Id);
                entry.Player2 = Player.GetById(Database.Connection, (int)game.Player2Id);

                viewModel.Entries.Add(entry);
            }

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
            viewModel.Player1 = Player.GetById(Database.Connection, (int)viewModel.Game.Player1Id);
            viewModel.Player2 = Player.GetById(Database.Connection, (int)viewModel.Game.Player2Id);

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
    }
}