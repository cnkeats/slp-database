using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using System.Collections.Generic;

namespace SlippiStats.ViewModels
{
    public class PlayerListViewModel : SlippiStatsViewModel
    {
        public string PlayerFilter { get; set; }

        public List<PlayerListEntry> PlayerListEntries { get; set; }

        public PlayerListViewModel() : base()
        {
            PlayerListEntries = new List<PlayerListEntry>();
        }
    }

    public class PlayerListEntry
    {
        public Player Player { get; set; }

        public int GamesPlayed { get; set; }

        public int GamesWon { get; set; }

        public List<Character> Mains { get; set; }

        public PlayerListEntry()
        {
            Mains = new List<Character>();
        }
    }
}