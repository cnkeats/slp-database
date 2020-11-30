
using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using System.Collections.Generic;

namespace SlippiStats.ViewModels
{
    public class PlayerListViewModel : SlippiStatsViewModel
    {
        public string PlayerFilter { get; set; }

        public List<Player> Players { get; set; }

        public PlayerListViewModel() : base()
        {
            Players = new List<Player>();
        }
    }
}