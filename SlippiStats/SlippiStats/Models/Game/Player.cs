using SlippiStats.GameDataEnums;
using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace SlippiStats.Models
{
    public class Player
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public string ConnectCode { get; set; }

        public string DiscordCode { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }

        public bool IsDeleted
        {
            get => Deleted != null;
            set => Deleted = value ? DateTime.Now : (DateTime?)null;
        }

        public Player()
        {
            Created = DateTime.Now;
        }

        public Player(IDataReader dataReader, string prefix = "")
        {
            Id = dataReader.GetValue<int>(prefix + nameof(Id));
            Name = dataReader.GetValue<string>(prefix + nameof(Name));
            ConnectCode = dataReader.GetValue<string>(prefix + nameof(ConnectCode));
            DiscordCode = dataReader.GetValue<string>(prefix + nameof(DiscordCode));
            Created = dataReader.GetValue<DateTime>(prefix + nameof(Created));
            Updated = dataReader.GetValue<DateTime?>(prefix + nameof(Updated));
            Deleted = dataReader.GetValue<DateTime?>(prefix + nameof(Deleted));
        }

        public static string GetPlayerName(SlpReplay slpReplay, int playerIndex)
        {
            IDictionary<string, SlpMetadataPlayer> players = slpReplay.MetaData.Players;
            string key = playerIndex.ToString();

            if (players.Keys.Contains(key))
            {
                if (players[key].Names.Netplay != null && players[key].Names.Netplay.Trim().Length > 0)
                {
                    return players[key].Names.Netplay;
                }
                else if (players[key].Names.Code != null && players[key].Names.Code.Trim().Length > 0)
                {
                    return players[key].Names.Code;
                }
                else
                {
                    return slpReplay.Settings.Players[0].Type == PlayerType.HUMAN ? "P" + (playerIndex + 1) : "CPU" + (playerIndex + 1);
                }
            }

            return null;
        }
        public static Player GetById(IDbConnection connection, int playerId)
        {
            Player player = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(GetById)}",
                new { playerId });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                player = new Player(reader);
            }

            return player;
        }

        public static Player GetByConnectCode(IDbConnection connection, string connectCode)
        {
            Player player = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(GetByConnectCode)}",
                new { connectCode });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                player = new Player(reader);
            }

            return player;
        }

        public static Player GetByPlayerName(IDbConnection connection, string playerName)
        {
            Player player = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(GetByPlayerName)}",
                new { playerName });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                player = new Player(reader);
            }

            return player;
        }

        public static List<Player> GetList(IDbConnection connection, bool includeAnonymous = false)
        {
            List<Player> players = new List<Player>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(GetList)}",
                new { includeAnonymous });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                players.Add(new Player(reader));
            }

            return players;
        }

        public static List<Player> GetListByFilters(IDbConnection connection, string playerFilter)
        {
            List<Player> players = new List<Player>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(GetListByFilters)}",
                new { playerFilter });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                players.Add(new Player(reader));
            }

            return players;
        }

        public static int GetGamesPlayedByPlayerId(IDbConnection connection, int playerId)
        {
            int gamesPlayed = 0;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(GetGamesPlayedByPlayerId)}",
                new { playerId });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                gamesPlayed = reader.GetValue<int>(nameof(gamesPlayed));
            }

            return gamesPlayed;
        }

        public static int GetGamesWonByPlayerId(IDbConnection connection, int playerId)
        {
            int gamesWon = 0;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(GetGamesWonByPlayerId)}",
                new { playerId });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                gamesWon = reader.GetValue<int>(nameof(gamesWon));
            }

            return gamesWon;
        }

        public static List<Character> GetCharacterMainsByPlayerId(IDbConnection connection, int playerId)
        {
            List<Character> characters = new List<Character>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(GetCharacterMainsByPlayerId)}",
                new { playerId });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                characters.Add((Character)reader.GetValue<int>("Id"));
            }

            return characters;
        }

        public static List<Character> GetPlayedCharactersByPlayerId(IDbConnection connection, int playerId)
        {
            List<Character> characters = new List<Character>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(GetPlayedCharactersByPlayerId)}",
                new { playerId });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                characters.Add((Character)reader.GetValue<int>("Id"));
            }

            return characters;
        }

        public void Save(IDbConnection connection)
        {
            if (Id == 0)
            {
                Insert(connection);
            }
            else
            {
                Update(connection);
            }
        }

        private void Insert(IDbConnection connection)
        {
            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(Insert)}",
                new
                {
                    Name,
                    ConnectCode,
                    DiscordCode,
                    Created,
                    Updated,
                    Deleted
                });

            Id = (int)command.ExecuteScalar();
        }

        private void Update(IDbConnection connection)
        {
            Updated = DateTime.Now;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(Update)}",
                new
                {
                    Id,
                    Name,
                    ConnectCode,
                    DiscordCode,
                    Created,
                    Updated,
                    Deleted
                });

            command.ExecuteNonQuery();
        }
    }
}
