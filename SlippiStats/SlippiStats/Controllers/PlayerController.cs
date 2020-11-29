﻿using Microsoft.AspNetCore.Http;
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
    public class PlayerController : ApplicationController
    {
        public PlayerController(ApplicationSettings settings, ApplicationDatabase database) : base(settings, database)
        {

        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        public IActionResult List()
        {
            PlayerListViewModel viewModel = new PlayerListViewModel();
            viewModel.Players = Player.GetList(Database.Connection);
            return View(viewModel);
        }

       [HttpPost]
       public IActionResult List(PlayerListViewModel viewModel)
        {
            viewModel.Players = Player.GetList(Database.Connection);
            return View(viewModel);
        }

        public IActionResult Profile(int id)
        {
            PlayerProfileViewModel viewModel = new PlayerProfileViewModel();
            viewModel.Player = Player.GetById(Database.Connection, id);
            viewModel.PlayedCharacters = Player.GetPlayedCharactersByPlayerId(Database.Connection, id);
            viewModel.MatchupResults = MatchupResults.GetListByPlayerId(Database.Connection, id);

            List<Game> games = Game.GetList(Database.Connection);

            viewModel.Entries = new List<GameEntryView>();
            foreach (Game game in games)
            {
                GameEntryView entry = new GameEntryView();
                entry.Game = game;
                entry.Player1 = Player.GetById(Database.Connection, (int)game.Player1Id);
                entry.Player2 = Player.GetById(Database.Connection, (int)game.Player2Id);

                viewModel.Entries.Add(entry);
            }

            return View(viewModel);
        }
    }
}
