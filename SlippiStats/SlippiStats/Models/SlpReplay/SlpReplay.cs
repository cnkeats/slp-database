using SlippiStats.GameDataEnums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlippiStats.Models
{
    public class SlpReplay
    {
        public int Id { get; private set; }

        public string FileName { get; set; }

        public string Hash { get; set; }

        public string FileSource { get; set; }

        public SlpSettings Settings { get; set; }

        public SlpMetadata MetaData { get; set; }

        public SlpStats Stats { get; set; }

        public List<float?> LatestFramePercents { get; set; }

        public SlpGameEnd GameEnd { get; set; }

        public long? StartingSeed { get; set; }

        public static bool?[] DetermineVictories(SlpReplay slpReplay)
        {
            int playerCount = slpReplay.Settings.Players.Count;

            // Games with more than 2 players are missing the required stats to calculate who won
            if (playerCount > 2)
            {
                return new bool?[] { null, null, null, null };
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
            bool?[] output = new bool?[4];

            // In replays where the "Keep playing while paused" code is enabled before v3.7.1, LRAS index is unreliable. Return null instead.
            if (new Version(slpReplay.Settings.SlpVersion) < new Version("3.7.1"))
            {
                output[0] = null;
                output[1] = null;
                output[2] = null;
                output[3] = null;

                return output;
            }

            int index = 0;
            foreach (SlpSettingsPlayer player in slpReplay.Settings.Players)
            {
                if (slpReplay.Settings.IsTeams)
                {
                    // If that player's index is valid, they lose if their team matches the team of the LRAS Initiator
                    output[index] = slpReplay.Settings.Players.Count >= index + 1 && player.TeamId != slpReplay.Settings.Players.Where(p => p.PlayerIndex == (int)slpReplay.GameEnd.LRASInitiatorIndex).First().TeamId ? true : false;
                }
                else
                {
                    // If not teams, players lose if they were the one to LRAS.
                    output[index] = slpReplay.GameEnd.LRASInitiatorIndex != (LRAS)index;
                }
                index++;
            }

            return output;
        }

        private static bool?[] DetermineVictories_GAME(SlpReplay slpReplay)
        {
            bool?[] output = new bool?[4];

            // Any player that is left alive will share the same team. Use this to determine the winning team.
            Team winningTeam = slpReplay.Settings.Players[slpReplay.Stats.Stocks.Where(s => s.DeathAnimation == null).First().PlayerIndex].TeamId;

            int index = 0;
            foreach (SlpSettingsPlayer player in slpReplay.Settings.Players)
            {
                if (slpReplay.Settings.IsTeams)
                {
                    // If teams, players win if they share a team with any player that remains alive
                    output[index] = player.TeamId == winningTeam;
                }
                else
                {
                    // If not teams, players win if they didn't die on their last stock
                    output[index] = !slpReplay.Stats.Stocks.Any(s => s.PlayerIndex == index && s.Count == 1 && s.DeathAnimation != null);
                }
                index++;
            }

            return output;
        }

        private static bool?[] DetermineVictories_TIME(SlpReplay slpReplay)
        {
            bool?[] output = new bool?[4];

            // The player that is left alive with the highest stock count and then lowest percent is on the winning team.
            // In the event of a tie, I haven't worked out the actual answer and so this isn't reliable.
            Team winningTeam = slpReplay.Settings.Players[slpReplay.Stats.Stocks.Where(s => s.DeathAnimation != null).OrderByDescending(s => s.Count).ThenBy(s => s.CurrentPercent).First().PlayerIndex].TeamId;

            int index = 0;
            foreach (SlpSettingsPlayer player in slpReplay.Settings.Players)
            {
                if (slpReplay.Settings.IsTeams)
                {
                    // If teams, players win if they share a team with any player that remains alive
                    output[index] = player.TeamId == winningTeam;
                }
                else
                {
                    // If not teams, players win if they are alive with the highest stock and lowest percent.
                    // In the event of a tie, I haven't programmed it in and so this isn't reliable.
                    output[index] = slpReplay.Stats.Stocks.Where(s => s.DeathAnimation != null).OrderByDescending(s => s.Count).ThenBy(s => s.CurrentPercent).First().PlayerIndex == index;
                }
                index++;
            }

            return output;
        }
    }
}