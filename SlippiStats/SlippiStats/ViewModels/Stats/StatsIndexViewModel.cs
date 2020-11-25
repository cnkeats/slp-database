
using SlippiStats.Models;
using System.Collections.Generic;

namespace SlippiStats.ViewModels
{
    public class StatsIndexViewModel : SlippiStatsViewModel
    {
        public string PlayerFilter { get; set; }

        public string CharacterFilter { get; set; }

        public string StageFilter { get; set; }

        public List<Game> Games { get; set; }

        public StatsIndexViewModel()
        {
            Games = new List<Game>();
        }
    }
}