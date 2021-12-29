using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using System.Collections.Generic;

namespace SlippiStats.Controllers
{
    public class TournamentListViewModel : SlippiStatsViewModel
    {
        public List<Tournament> Entries { get; set; }

        public TournamentListViewModel() : base()
        {
            Entries = new List<Tournament>();
        }
    }
}