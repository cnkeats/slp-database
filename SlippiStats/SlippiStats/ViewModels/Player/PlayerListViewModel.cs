
using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using System.Collections.Generic;

namespace SlippiStats.ViewModels
{
    public class PlayerListViewModel : SlippiStatsViewModel
    {
        public string PlayerFilter1 { get; set; }

        public string PlayerFilter2 { get; set; }

        public Character? CharacterFilter1 { get; set; }

        public Character? CharacterFilter2 { get; set; }

        public Stage? StageFilter { get; set; }

        public List<Player> Players { get; set; }

        public PlayerListViewModel() : base()
        {
            Players = new List<Player>();
        }
    }
}