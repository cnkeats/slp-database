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
            Game game = new Game();
            game.Player1 = gameReplay.MetaData.Players["0"].Names.Netplay;
            game.Player2 = gameReplay.MetaData.Players["1"].Names.Netplay;
            game.GameLength = gameReplay.MetaData.LastFrame;
            game.Save(Database.Connection);

            return JsonConvert.SerializeObject(game);
        }
    }
}
