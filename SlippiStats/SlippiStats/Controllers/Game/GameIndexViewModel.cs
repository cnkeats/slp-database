using SlippiStats.Models;

namespace SlippiStats.Controllers
{
    public class GameIndexViewModel
    {
        public string Message { get; set; }

        public string Error { get; set; }

        public Game Game { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public ReplayFile ReplayFile { get; set; }

        public GameIndexViewModel()
        {

        }
    }
}