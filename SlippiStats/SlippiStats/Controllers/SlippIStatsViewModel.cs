using SlippiStats.Extensions;
using SlippiStats.GameDataEnums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlippiStats.Controllers
{
    public class SlippiStatsViewModel
    {
        public string Message { get; set; }

        public string Error { get; set; }

        public List<Character> Characters { get; set; }

        public List<Stage> Stages { get; set; }

        public SlippiStatsViewModel()
        {
            Characters = Enum.GetValues(typeof(Character)).OfType<Character>().OrderBy(c => c.GetTierPlacement()).ToList();
            Stages = Enum.GetValues(typeof(Stage)).OfType<Stage>().OrderBy(s => s.GetDisplayName()).ToList();
        }
    }
}