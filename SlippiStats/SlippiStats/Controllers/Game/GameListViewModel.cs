using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using System.Collections.Generic;

namespace SlippiStats.Controllers
{
    public class GameListViewModel : SlippiStatsViewModel
    {
        public string PlayerFilter1 { get; set; }

        public string PlayerFilter2 { get; set; }

        public Character? CharacterFilter1 { get; set; }

        public Character? CharacterFilter2 { get; set; }

        public Stage? StageFilter { get; set; }

        public List<GameListEntry> Entries { get; set; }

        public GameListViewModel() : base()
        {
            Entries = new List<GameListEntry>();
        }
    }

    public class GameListEntry
    {
        public Game Game { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }
    }
}