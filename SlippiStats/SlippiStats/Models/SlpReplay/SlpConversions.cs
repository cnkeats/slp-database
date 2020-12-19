using System.Collections.Generic;

namespace SlippiStats.Models
{
    public class SlpConversions
    {
        public int PlayerIndex { get; set; }

        public int OpponentIndex { get; set; }

        public int StartFrame { get; set; }

        public int EndFrame { get; set; }

        public float StartPercent { get; set; }

        public float EndPercent { get; set; }

        public List<SlpMove> Moves { get; set; }

        public bool DidKill { get; set; }

        public string OpeningType { get; set; }
    }
}