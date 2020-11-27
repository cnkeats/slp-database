
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

        public List<Game> Games { get; set; }

        public PlayerProfileViewModel() : base()
        {
            Games = new List<Game>();
            PlayedCharacters = new List<Character>();
            MatchupResults = new List<MatchupResults>();
        }
    }
}