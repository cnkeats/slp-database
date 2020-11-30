using System.Collections.Generic;

namespace SlippiStats.Models
{
    public class SlpReplay
    {
        public int Id { get; private set; }

        public string FileName { get; set; }

        public string Hash { get; set; }

        public SlpSettings Settings { get; set; }

        public SlpMetadata MetaData { get; set; }

        public List<int> Stats { get; set; }

        public List<float?> LatestFramePercents { get; set; }

        public SlpGameEnd GameEnd { get; set; }

        public long? StartingSeed { get; set; }

        public static bool?[] DetermineVictories(SlpReplay slpReplay)
        {
            // Only worry about 1v1 for now
            if (slpReplay.Settings.Players.Count > 2)
            {
                return null;
            }

            // If the game doesn't have stats data, we can't determine the victor
            if (slpReplay.Stats.Count != 2)
            {
                return null;
            }

            // Games that don't contain game end mthod data are not supported
            GameEndMethod gameEndMethod = slpReplay.GameEnd.GameEndMethod;

            switch (gameEndMethod)
            {
                // Someone did LRAS or something else weird happened
                case GameEndMethod.NO_CONTEST:
                    return DetermineVictories_LRAS(slpReplay);
                case GameEndMethod.GAME:
                    return DetermineVictories_GAME(slpReplay);
                case GameEndMethod.TIME:
                    return DetermineVictories_TIME(slpReplay);
                default:
                    break;
            }

            // This probably shouldn't happen lol
            return null;
        }

        private static bool?[] DetermineVictories_LRAS(SlpReplay slpReplay)
        {
            bool?[] output = new bool?[2];

            output[0] = slpReplay.GameEnd.LRASInitiatorIndex != LRAS.PLAYER1;
            output[1] = slpReplay.GameEnd.LRASInitiatorIndex != LRAS.PLAYER2;

            return output;
        }

        private static bool?[] DetermineVictories_GAME(SlpReplay slpReplay)
        {
            bool?[] output = new bool?[2];

            int p1EndingStocks = slpReplay.Settings.Players[0].StartStocks - slpReplay.Stats[1];
            int p2EndingStocks = slpReplay.Settings.Players[1].StartStocks - slpReplay.Stats[0];

            float p1EndingPercentage = (float)slpReplay.LatestFramePercents[0];
            float p2EndingPercentage = (float)slpReplay.LatestFramePercents[1];

            // Player 1 had more stocks remaining
            if (p1EndingStocks > p2EndingStocks)
            {
                output[0] = true;
                output[1] = false;
            }
            // Player 2 had more stocks remaining
            else if (p1EndingStocks < p2EndingStocks)
            {
                output[0] = false;
                output[1] = true;
            }
            // Player 1 had less percentage
            // Decided by a 1v1
            // TODO: Swap out bool? for a 4-state type
            else if (p1EndingPercentage < p2EndingPercentage)
            {
                output[0] = false;
                output[1] = false;
            }
            // Player 2 had less percentage
            // Decided by a 1v1
            // TODO: Swap out bool? for a 4-state type
            else if (p1EndingPercentage > p2EndingPercentage)
            {
                output[0] = false;
                output[1] = false;
            }
            // Decided by a 1v1
            // TODO: Swap out bool? for a 4-state type
            else
            {
                output[0] = false;
                output[1] = false;
            }

            return output;
        }

        private static bool?[] DetermineVictories_TIME(SlpReplay slpReplay)
        {
            bool?[] output = new bool?[2];

            int p1EndingStocks = slpReplay.Settings.Players[0].StartStocks - slpReplay.Stats[1];
            int p2EndingStocks = slpReplay.Settings.Players[1].StartStocks - slpReplay.Stats[0];

            float p1EndingPercentage = (float)slpReplay.LatestFramePercents[0];
            float p2EndingPercentage = (float)slpReplay.LatestFramePercents[1];

            // Player 1 had more stocks remaining
            if (p1EndingStocks > p2EndingStocks)
            {
                output[0] = true;
                output[1] = false;
            }
            // Player 2 had more stocks remaining
            else if (p1EndingStocks < p2EndingStocks)
            {
                output[0] = false;
                output[1] = true;
            }
            // Player 1 had less percentage
            else if (p1EndingPercentage < p2EndingPercentage)
            {
                output[0] = true;
                output[1] = false;
            }
            // Player 2 had less percentage
            else if (p1EndingPercentage > p2EndingPercentage)
            {
                output[0] = false;
                output[1] = true;
            }
            // Decided by a 1v1
            // TODO: Swap out bool? for a 4-state type
            else
            {
                output[0] = false;
                output[1] = false;
            }

            return output;
        }
    }
}
