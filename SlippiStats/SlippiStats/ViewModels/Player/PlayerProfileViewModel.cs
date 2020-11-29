
using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using System.Collections.Generic;

namespace SlippiStats.ViewModels
{
    public class PlayerProfileViewModel : SlippiStatsViewModel
    {
        public Player Player { get; set; }

        public string PlayerFilter { get; set; }

        public Character? CharacterFilter1 { get; set; }

        public Character? CharacterFilter2 { get; set; }

        public Stage? StageFilter { get; set; }

        public List<Character> PlayedCharacters { get; set; }

        public List<MatchupResults> MatchupResults { get; set; }

        public List<GameEntryView> Entries { get; set; }

        public PlayerProfileViewModel() : base()
        {
            Entries = new List<GameEntryView>();
            PlayedCharacters = new List<Character>();
            MatchupResults = new List<MatchupResults>();
        }
    }
}