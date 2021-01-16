using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlippiStats.Configuration;
using SlippiStats.Models;
using SlippiStats.Models.ReplaySubmission;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlippiStats.Controllers
{
    public class APIController : ApplicationController
    {
        public APIController(ApplicationSettings settings, ApplicationDatabase database) : base(settings, database)
        {

        }

        [AllowAnonymous]
        [HttpPost]
        public ReplaySubmitResponse SubmitReplayFile([FromForm] ReplaySubmission replaySubmission)
        {
            ReplaySubmitResponse response = new ReplaySubmitResponse();

            Game game = Game.GetById(Database.Connection, replaySubmission.GameId);
            if (game == null)
            {
                response.Success = false;
                response.Message = String.Format("No matching game found for that replay. Replay {0} NOT uploaded.", replaySubmission.File.FileName);
                return response;
            }

            ReplayFile replayFile = ReplayFile.GetByGameIdUploaderId(Database.Connection, game.Id, replaySubmission.UploaderId);
            if (replayFile != null)
            {
                response.Success = true;
                response.Message = String.Format("A matching replay for Game #{0} was already found. Replay {1} NOT uploaded.", game.Id, replaySubmission.File.FileName);
                return response;
            }

            replayFile = new ReplayFile(replaySubmission.File, game.Id);
            replayFile.Save(Database.Connection);

            response.Success = true;
            response.Message = String.Format("Replay successfully saved for Game #{0}", game.Id);
            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        public GameSubmitResponse SubmitGame([FromBody] SlpReplay slpReplay)
        {
            GameSubmitResponse response = new GameSubmitResponse();

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

                if (slpReplay.GameEnd == null)
                {
                    response.Success = false;
                    response.Message = "Support for games without GameEnd data is not currently available.";
                    return response;
                }

                Game game = ProcessGameFromSlpReplay(slpReplay);

                if (game == null)
                {
                    response.Success = false;
                    response.Message = "There was an error processing the game metadata.";
                    return response;
                }

                response.Success = true;
                response.Message = String.Format("Game #{0} saved successfully.", game.Id);
                response.GameId = game.Id;
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

        private Game ProcessGameFromSlpReplay(SlpReplay slpReplay)
        {
            List<string> ignoredNames = new[] { "P1", "P2", "P3", "P4", "CPU1", "CPU2", "CPU3", "CPU4" }.ToList();
            List<Player> players = new List<Player>();
            if (slpReplay.MetaData != null)
            {
                int playerIndex = 0;
                foreach (SlpMetadataPlayer playerMetadata in slpReplay.MetaData.GetPlayers())
                {
                    if (playerMetadata.Names == null)
                    {
                        return null;
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

            // TODO: Better duplication checking
            Game game = Game.GetByHash(Database.Connection, slpReplay.Hash);
            if (game == null)
            {
                game = new Game(slpReplay);

                game.Player1Id = players.Count > 0 ? players[0]?.Id : null;
                game.Player2Id = players.Count > 1 ? players[1]?.Id : null;
                game.Player3Id = players.Count > 2 ? players[2]?.Id : null;
                game.Player4Id = players.Count > 3 ? players[3]?.Id : null;
            }
            else
            {
                game.Version = new Version(slpReplay.Settings.SlpVersion);
                game.FileName = slpReplay.FileName;
                game.Hash = slpReplay.Hash;
                game.StartAt = slpReplay.MetaData.StartAt;
                game.StartingSeed = slpReplay.StartingSeed;
                game.GameLength = slpReplay.MetaData.LastFrame;
                game.Platform = slpReplay.MetaData.PlayedOn;
            }
            
            game.Save(Database.Connection);

            return game;
        }
    }

    public class GameSubmitResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public int GameId { get; set; }
    }

    public class ReplaySubmitResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}