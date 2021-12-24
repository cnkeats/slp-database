namespace SlippiStats.Models
{
    public class SlpOverall
    {
        public int PlayerIndex { get; set; }

        public int OpponentIndex { get; set; }

        public SlpInputCounts InputCounts { get; set; }

        public int ConversionCount { get; set; }

        public decimal TotalDamage { get; set; }

        public int KillCount { get; set; }

        public SlpStatsRatio SuccessfulConversions { get; set; }

        public SlpStatsRatio InputsPerMinute { get; set; }

        public SlpStatsRatio DigitalInputsPerMinute { get; set; }

        public SlpStatsRatio OpeningsPerKill { get; set; }

        public SlpStatsRatio DamagePerOpening { get; set; }

        public SlpStatsRatio NeutralWinRatio { get; set; }

        public SlpStatsRatio CounterHitRatio { get; set; }

        public SlpStatsRatio BeneficialTradeRatio { get; set; }
    }
}