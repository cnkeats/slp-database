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
            return JsonConvert.SerializeObject(gameReplay);
        }
    }
}
