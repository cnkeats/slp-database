using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlippiStats.Configuration;
using SlippiStats.Models;

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
            Game game = Game.GetByHash(Database.Connection, gameReplay.Hash);
            if (game == null)
            {
                game = new Game(gameReplay);
                game.Save(Database.Connection);
            }

            return JsonConvert.SerializeObject(game);
        }
    }
}
