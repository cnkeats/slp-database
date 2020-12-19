using SlippiStats.GameDataEnums;
using System.Collections.Generic;

namespace SlippiStats.Models
{
    public class SlpStats
    {
        public int LastFrame { get; set; }

        public int PlayableFrameCount { get; set; }

        public List<SlpStock> Stocks { get; set; }

        public List<SlpConversions> Conversions { get; set; }

        public List<SlpCombo> Combos { get; set; }

        public List<SlpActionCount> ActionCounts { get; set; }

        public List<SlpOverall> Overall { get; set; }

        public bool GameComplete { get; set; }
    }
}