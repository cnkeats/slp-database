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

            if (gameReplay.Stats.Count == 0 && false)
            {
                return JsonConvert.SerializeObject("Support for modes other than Stock is not currently available.");
            }

            try
            {
                Game game = Game.GetByHash(Database.Connection, "");
                if (game == null || true)
                {
                    game = new Game(gameReplay);
                    game.Save(Database.Connection);
                }
                return JsonConvert.SerializeObject(game);
            }
            catch(Exception e)
            {
                return JsonConvert.SerializeObject(String.Format("There was an error processing your replay.\n{0}\n", e.Message, e.StackTrace));
            }
        }
    }
}
