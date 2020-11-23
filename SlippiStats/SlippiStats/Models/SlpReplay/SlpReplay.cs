using System.Collections.Generic;

namespace SlippiStats.Models
{
    public class SlpReplay
    {
        public int Id { get; private set; }

        public string Hash { get; set; }

        public SlpSettings Settings { get; set; }

        public SlpMetadata MetaData { get; set; }

        public List<int> Stats { get; set; }

        public List<float> LatestFramePercents { get; set; }

        public static int DetermineWinner(SlpReplay replay)
        {
            bool p1LRAS = false;
            bool p2LRAS = false;
            int p1Stocks = replay.Stats[0];
            int p2Stocks = replay.Stats[1];
            float p1Percent = replay.LatestFramePercents[0];
            float p2Percent = replay.LatestFramePercents[1];

            if (p2LRAS)
            {
                return 0;
            }
            else if (p1LRAS)
            {
                return 1;
            }
            else if (p1Stocks > p2Stocks)
            {
                return 0;
            }
            else if (p1Stocks < p2Stocks)
            {
                return 1;
            }
            else if (p1Percent < p2Percent)
            {
                return 0;
            }
            else if (p1Percent > p2Percent)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
