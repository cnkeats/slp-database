using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpPost]
        public string Submit([FromBody] SlpReplay gameReplay)
        {
            if (gameReplay == null)
            {
                return JsonConvert.SerializeObject("Replay was null.");
            }

            if (gameReplay.Settings.Players.Count != 2)
            {
                return JsonConvert.SerializeObject("Support for games with more than 2 players is not currently available.");
            }

            if (gameReplay.Stats.Count == 0)
            {
                return JsonConvert.SerializeObject("Support for modes other than Stock is not currently available.");
            }

            try
            {
                Game game = Game.GetByHash(Database.Connection, gameReplay.Hash);
                if (game == null || true)
                {
                    game = new Game(gameReplay);
                    game.Save(Database.Connection);
                }
                return JsonConvert.SerializeObject(game);
            }
            catch(Exception e)
            {
                return JsonConvert.SerializeObject(new
                {
                    message = "There was an error processing your replay.",
                    error = e.Message,
                    stacktrace = e.StackTrace
                });
            }
        }
    }
}
