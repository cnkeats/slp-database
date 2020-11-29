
using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using System.Collections.Generic;

namespace SlippiStats.ViewModels
{
    public class StatsIndexViewModel : SlippiStatsViewModel
    {
        public string PlayerFilter1 { get; set; }

        public string PlayerFilter2 { get; set; }

        public Character? CharacterFilter1 { get; set; }

        public Character? CharacterFilter2 { get; set; }

        public Stage? StageFilter { get; set; }

        public List<GameEntryView> Entries { get; set; }

        public StatsIndexViewModel() : base()
        {
            Entries = new List<GameEntryView>();
        }
    }

    public class GameEntryView
    {
        public Game Game { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }
    }
}