namespace SlippiStats.Models
{
    public class SlpStock
    {
        public int PlayerIndex { get; set; }

        public int OpponentIndex { get; set; }

        public int StartFrame { get; set; }

        public int StartPercent { get; set; }

        public float? EndPercent { get; set; }

        public float CurrentPercent { get; set; }

        public int Count { get; set; }

        public int? DeathAnimation { get; set; }
    }
}