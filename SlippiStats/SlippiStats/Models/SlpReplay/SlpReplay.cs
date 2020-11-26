using System.Collections.Generic;
using System.Linq;

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

        public static string DetermineWinner(SlpReplay replay)
        {

            SlpSettingsPlayer p1 = replay.Settings.Players.Count > 0 ? replay.Settings.Players[0] : null;
            SlpSettingsPlayer p2 = replay.Settings.Players.Count > 1 ? replay.Settings.Players[1] : null;
            SlpSettingsPlayer p3 = replay.Settings.Players.Count > 2 ? replay.Settings.Players[2] : null;
            SlpSettingsPlayer p4 = replay.Settings.Players.Count > 3 ? replay.Settings.Players[3] : null;

            List<SlpSettingsPlayer> players = new List<SlpSettingsPlayer>();
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);

            int?[] playerStocks = new int?[4];
            playerStocks[0] = replay.Stats.Count > 0 ? replay.Stats[0] : (int?)null;
            playerStocks[1] = replay.Stats.Count > 1 ? replay.Stats[1] : (int?)null;
            playerStocks[2] = replay.Stats.Count > 2 ? replay.Stats[2] : (int?)null;
            playerStocks[3] = replay.Stats.Count > 3 ? replay.Stats[3] : (int?)null;

            if (replay.Settings.IsTeams)
            {
                return "TEAMS";
            }

            string output = "";
            foreach (SlpSettingsPlayer player in players)
            {
                if (player == null)
                {
                    output += "X";
                }
                else
                {
                    output += "U";
                }
            }

            // TODO: More robust default for older replays
            GameEndMethod gameEndMethod = GameEndMethod.GAME;
            if (replay.GameEnd != null)
            {
                gameEndMethod = replay.GameEnd.GameEndMethod;
            }

            char[] array = output.ToCharArray();
            switch (gameEndMethod) {
                case GameEndMethod.NO_CONTEST:
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (replay.GameEnd.LRASInitiatorIndex != (LRAS)i)
                        {
                            array[i] = players[i] != null ? '1' : array[i];
                        }
                        else
                        {
                            array[i] = players[i] != null ? '0' : array[i];
                        }
                    }
                    break;
                case GameEndMethod.GAME:
                    for (int i = 0; i < players.Count; i++)
                    {
                        array[i] = players[i] != null ? playerStocks[i] == players[i].StartStocks ? '1' : '0' : array[i];
                    }
                    break;
                // Worry about this later
                case GameEndMethod.TIME:
                    array[0] = 'T';
                    array[1] = 'I';
                    array[2] = 'M';
                    array[3] = 'E';
                    break;
                default:
                    array[0] = 'D';
                    array[1] = 'F';
                    array[2] = 'L';
                    array[3] = 'T';
                    break;
            }
            output = new string(array);

            return output;
        }

    }
}
