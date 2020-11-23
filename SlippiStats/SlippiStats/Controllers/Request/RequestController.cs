using Controllers.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlippiStats.Configuration;
using SlippiStats.Models;

namespace SlippiStats.Controllers
{
    public class RequestController : ApplicationController
    {
        public RequestController(ApplicationSettings settings, ApplicationDatabase database) : base(settings, database)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MetaStats()
        {
            return View();
        }

        [HttpPost]
        public string SubmitGame([FromBody] SlpReplay gameReplay)
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
