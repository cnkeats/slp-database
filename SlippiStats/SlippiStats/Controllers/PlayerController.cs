using Microsoft.AspNetCore.Mvc;
using SlippiStats.Configuration;
using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using SlippiStats.ViewModels;
using System.Collections.Generic;

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
            viewModel.Players = Player.GetListByFilters(Database.Connection, viewModel.PlayerFilter);
            return View(viewModel);
        }

        public IActionResult Profile(int id, Character? character = null, string opponentFilter = null, Character? opponentCharacter = null, Stage? stage = null, int? opponentPlayerId = null)
        {
            PlayerProfileViewModel viewModel = new PlayerProfileViewModel();
            viewModel.OpponentFilter = opponentFilter;
            viewModel.Player = Player.GetById(Database.Connection, id);
            viewModel.PlayerProfile = PlayerProfile.GetByPlayerId(Database.Connection, id, character, opponentFilter, opponentCharacter);

            if (viewModel.PlayerProfile != null)
            {
                viewModel.PlayerProfile.FavoriteOpponent = Player.GetById(Database.Connection, viewModel.PlayerProfile.FavoriteOpponentId);
            }

            if (viewModel.Player == null)
            {
                return RedirectToAction(nameof(List));
            }

            viewModel.PlayedCharacters = Player.GetPlayedCharactersByPlayerId(Database.Connection, id);
            //viewModel.MatchupResults = MatchupResults.GetListByPlayerId(Database.Connection, id);
            viewModel.MatchupResults = MatchupResults.GetListByPlayerIdFilters(Database.Connection, id, null, opponentFilter, null);

            List<Game> games = Game.GetListByPlayerIdFilters(Database.Connection, id, character, opponentFilter, opponentCharacter, stage, opponentPlayerId);

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
