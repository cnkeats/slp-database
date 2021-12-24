using SlippiStats.GameDataEnums;
using SlippiStats.Util;
using System.Collections.Generic;
using System.Data;

namespace SlippiStats.Models
{
    public class GamePlayers
    {
        public Game Game { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Player Player3 { get; set; }

        public Player Player4 { get; set; }

        public GamePlayers(IDataReader dataReader)
        {
            Game = new Game(dataReader);
            Player1 = new Player(dataReader, "p1");
            Player2 = new Player(dataReader, "p2");
            //Player3 = new Player(dataReader, "p3");
            //Player4 = new Player(dataReader, "p4");
        }

        public static List<GamePlayers> GetList(IDbConnection connection, bool includeAnonymous = false)
        {
            List<GamePlayers> GamePlayers = new List<GamePlayers>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetList)}",
                new { includeAnonymous });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                GamePlayers.Add(new GamePlayers(reader));
            }

            return GamePlayers;
        }

        public static List<GamePlayers> GetListByFilters(IDbConnection connection, string playerName1 = null, string playerName2 = null, Character? character1 = null, Character? character2 = null, Stage? stage = null, bool includeAnonymous = false)
        {
            List<GamePlayers> GamePlayers = new List<GamePlayers>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetListByFilters)}",
                new { playerName1, playerName2, character1, character2, stage, includeAnonymous });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                GamePlayers.Add(new GamePlayers(reader));
            }

            return GamePlayers;
        }
    }
}
