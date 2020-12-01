using SlippiStats.GameDataEnums;
using SlippiStats.Util;
using System.Collections.Generic;
using System.Data;

namespace SlippiStats.Models
{
    public class MatchupResults
    {
        public Character Character { get; set; }

        public Character OpponentCharacter { get; set; }

        public int GamesPlayed { get; set; }

        public int Victories { get; set; }

        public int Losses { get; set; }


        public MatchupResults()
        {

        }

        private MatchupResults(IDataReader dataReader)
        {
            Character = (Character)dataReader.GetValue<int>(nameof(Character));
            OpponentCharacter = (Character)dataReader.GetValue<int>(nameof(OpponentCharacter));
            GamesPlayed = dataReader.GetValue<int>(nameof(GamesPlayed));
        }

        public static List<MatchupResults> GetListByPlayerId(IDbConnection connection, int playerId)
        {
            List<MatchupResults> matchupResults = new List<MatchupResults>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(MatchupResults)}_{nameof(GetListByPlayerId)}",
                new { playerId });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                matchupResults.Add(new MatchupResults(reader));
            }

            return matchupResults;
        }

        public static List<MatchupResults> GetListByPlayerIdFilters(IDbConnection connection, int playerId, Character? characterFilter = null, string opponentFilter = null, Character? opponentCharacterFilter = null)
        {
            List<MatchupResults> matchupResults = new List<MatchupResults>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(MatchupResults)}_{nameof(GetListByPlayerIdFilters)}",
                new { playerId, characterFilter, opponentFilter, opponentCharacterFilter });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                matchupResults.Add(new MatchupResults(reader));
            }

            return matchupResults;
        }
    }
}
