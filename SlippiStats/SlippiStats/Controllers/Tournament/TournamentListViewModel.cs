using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using System.Collections.Generic;

namespace SlippiStats.Controllers
{
    public class TournamentIndexViewModel : SlippiStatsViewModel
    {
        public Tournament Tournament { get; set; }

        public List<Game> Entries { get; set; }

        public TournamentIndexViewModel() : base()
        {
            Entries = new List<Game>();
        }
    }
}