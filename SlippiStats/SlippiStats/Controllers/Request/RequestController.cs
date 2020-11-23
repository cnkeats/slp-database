using Controllers.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlippiStats.Models;

namespace SlippiStats.Controllers
{
    public class RequestController : Controller
    {
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
            return JsonConvert.SerializeObject(game);
        }
    }
}
