﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SlippiStats.Configuration;
using SlippiStats.Models;
using SlippiStats.Models.ReplaySubmission;
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

        [HttpPost]
        public GameSubmitResponse SubmitFile(ReplaySubmission replaySubmission)
        {
            GameSubmitResponse response = new GameSubmitResponse();
            response.Message = "Data recieved.";
            response.Success = true;

            return response;
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        [HttpPost]
        public GameSubmitResponse Submit([FromBody] SlpReplay slpReplay)
        {
            GameSubmitResponse response = new GameSubmitResponse();


            byte[] fileBytes = System.IO.File.ReadAllBytes("D:\\SlippiReplays\\testing\\Game_20201129T235716.7z");

            try
            {
                if (slpReplay == null)
                {
                    response.Success = false;
                    response.Message = "Replay was null.";
                    return response;
                }

                if (slpReplay.Settings?.Players?.Count != 2)
                {
                    response.Success = false;
                    response.Message = "Support for games with more than 2 players is not currently available.";
                    return response;
                }

                if (slpReplay.Stats.Count == 0)
                {
                    response.Success = false;
                    response.Message = "Support for modes other than Stock is not currently available.";
                    return response;
                }

                if (slpReplay.GameEnd == null)
                {
                    response.Success = false;
                    response.Message = "Support for games without GameEnd data is not currently available.";
                    return response;
                }

                List<string> ignoredNames = new[] { "P1", "P2", "P3", "P4", "CPU1", "CPU2", "CPU3", "CPU4" }.ToList();
                List<Player> players = new List<Player>();
                if (slpReplay.MetaData != null)
                {
                    int playerIndex = 0;
                    foreach (SlpMetadataPlayer playerMetadata in slpReplay.MetaData.GetPlayers())
                    {
                        if (playerMetadata.Names == null)
                        {
                            response.Success = false;
                            response.Message = "Support for games without player name is not currently available. This is likely a spectated game and this issue should be taken care of soon.";
                            return response;
                        }

                        Player player = Player.GetByConnectCode(Database.Connection, playerMetadata.Names.Code);

                        if (player == null)
                        {
                            player = new Player();
                            player.Name = Player.GetPlayerName(slpReplay, playerIndex);
                            player.ConnectCode = playerMetadata.Names.Code;

                            if (!ignoredNames.Contains(player.Name))
                            {
                                player.Save(Database.Connection);
                            }
                            else
                            {
                                player = Player.GetByPlayerName(Database.Connection, player.Name);
                            }
                        }

                        players.Add(player);

                        playerIndex++;
                    }
                }

                if (slpReplay.GameEnd == null)
                {
                    response.Success = false;
                    response.Message = "Support for replays that are missing GameEnd data is not currently available.";
                    return response;
                }

                // TODO: Better duplication checking
                Game game = Game.GetByHash(Database.Connection, slpReplay.Hash);
                if (game == null)
                {
                    game = new Game(slpReplay);

                    game.Player1Id = players.Count > 0 ? players[0]?.Id : null;
                    game.Player2Id = players.Count > 1 ? players[1]?.Id : null;
                    game.Player3Id = players.Count > 2 ? players[2]?.Id : null;
                    game.Player4Id = players.Count > 3 ? players[3]?.Id : null;

                    game.Save(Database.Connection);

                    response.Message = string.Format("New game #{0} saved.", game.Id);
                }
                else
                {
                    game.FileName = slpReplay.FileName;
                    game.Hash = slpReplay.Hash;
                    game.StartAt = slpReplay.MetaData.StartAt;
                    game.StartingSeed = slpReplay.StartingSeed;
                    game.GameLength = slpReplay.MetaData.LastFrame;

                    game.Save(Database.Connection);

                    response.Message = string.Format("Existing game #{0} updated.", game.Id);
                }





                response.Success = true;
                response.Game = game;

                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                response.StackTrace = e.StackTrace;
            }

            return response;
        }
    }

    public class GameSubmitResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public Game Game { get; set; }
    }
}
