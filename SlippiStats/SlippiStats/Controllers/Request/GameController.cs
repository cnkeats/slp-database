using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlippiStats.Configuration;
using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using System;

namespace SlippiStats.Controllers
{
    public class GameController : ApplicationController
    {
        public GameController(ApplicationSettings settings, ApplicationDatabase database) : base(settings, database)
        {

        }

        [HttpPost]
        public GameSubmitResponse Submit([FromBody] SlpReplay gameReplay)
        {
            GameSubmitResponse response = new GameSubmitResponse();
            if (gameReplay == null)
            {
                response.Success = false;
                response.Message = "Replay was null.";
                return response;
            }

            if (gameReplay.Settings.Players.Count != 2)
            {
                response.Success = false;
                response.Message = "Support for games with more than 2 players is not currently available.";
                return response;
            }

            if (gameReplay.Stats.Count == 0)
            {
                response.Success = false;
                response.Message = "Support for modes other than Stock is not currently available.";
                return response;
            }

            try
            {
                Game game = Game.GetByHash(Database.Connection, gameReplay.Hash);
                if (game == null || true)
                {
                    game = new Game(gameReplay);
                    game.Save(Database.Connection);
                    
                    response.Message = string.Format("New game #{0} saved.", game.Id);
                }
                else
                {
                    response.Message = string.Format("Existing game #{0} updated. // TODO", game.Id);
                }

                response.Success = true;
                response.Game = game;

                return response;
            }
            catch(Exception e)
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
