﻿using Microsoft.AspNetCore.Mvc;
using SlippiStats.Configuration;
using SlippiStats.GameDataEnums;
using SlippiStats.Models;
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
            List<Player> players = Player.GetList(Database.Connection);
            viewModel.PlayerListEntries = new List<PlayerListEntry>();

            int count = 0;
            foreach (Player player in players)
            {
                PlayerListEntry entry = new PlayerListEntry();
                entry.Player = player;

                if (count < 100)
                {
                    entry.GamesPlayed = Player.GetGamesPlayedByPlayerId(Database.Connection, player.Id);
                    entry.GamesWon = Player.GetGamesWonByPlayerId(Database.Connection, player.Id);
                    entry.Mains = Player.GetCharacterMainsByPlayerId(Database.Connection, player.Id);
                }

                viewModel.PlayerListEntries.Add(entry);
                count++;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult List(PlayerListViewModel viewModel)
        {
            List<Player> players = Player.GetListByFilters(Database.Connection, viewModel.PlayerFilter);
            viewModel.PlayerListEntries = new List<PlayerListEntry>();

            int count = 0;
            foreach (Player player in players)
            {
                PlayerListEntry entry = new PlayerListEntry();
                entry.Player = player;

                if (count < 100)
                {
                    entry.GamesPlayed = Player.GetGamesPlayedByPlayerId(Database.Connection, player.Id);
                    entry.GamesWon = Player.GetGamesWonByPlayerId(Database.Connection, player.Id);
                    entry.Mains = Player.GetCharacterMainsByPlayerId(Database.Connection, player.Id);
                }

                viewModel.PlayerListEntries.Add(entry);
                count++;
            }
            return View(viewModel);
        }

        [Route("{controller}/{connectCode}")]
        public IActionResult ProfileByConnectCode(string connectCode)
        {
            Player player = Player.GetByConnectCode(Database.Connection, connectCode);
            player = player ?? Player.GetByConnectCode(Database.Connection, string.Format("{0}#{1}", connectCode[0..^3], connectCode.Substring(connectCode.Length - 3)));

            if (player != null)
            {
                return RedirectToAction(nameof(Profile), new { id = player.Id });
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Profile(int id, Character? character = null, string opponentFilter = null, Character? opponentCharacter = null, Stage? stage = null, int? opponentPlayerId = null)
        {
            PlayerProfileViewModel viewModel = new PlayerProfileViewModel();
            viewModel.OpponentFilter = opponentFilter;
            viewModel.Player = Player.GetById(Database.Connection, id);
            viewModel.PlayerProfile = PlayerProfile.GetByPlayerId(Database.Connection, id, character, opponentFilter, opponentCharacter);
            //viewModel.PlayerProfile = new PlayerProfile();

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
            viewModel.Entries = Game.GetListByPlayerIdFilters(Database.Connection, id, character, opponentFilter, opponentCharacter, stage, opponentPlayerId);
            

            return View(viewModel);
        }
    }
}