using SlippiStats.GameDataEnums;
using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace SlippiStats.Models
{
    public class Game
    {
        public int Id { get; private set; }

        public Character? Character1 { get; set; }

        public Character? Character2 { get; set; }

        public Character? Character3 { get; set; }

        public Character? Character4 { get; set; }

        public int? Player1Id { get; set; }

        public int? Player2Id { get; set; }

        public int? Player3Id { get; set; }

        public int? Player4Id { get; set; }

        public bool? Player1Victory { get; set; }

        public bool? Player2Victory { get; set; }

        public bool? Player3Victory { get; set; }

        public bool? Player4Victory { get; set; }

        public Stage Stage { get; set; }

        public GameMode? GameMode { get; set; }

        public GameEndMethod? GameEndMethod { get; set; }

        public DateTime StartAt { get; set; }

        public long? StartingSeed { get; set; }

        public int GameLength { get; set; }

        public string FileName { get; set; }

        public string Hash { get; set; }

        public string Platform { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }

        public bool IsDeleted
        {
            get => Deleted != null;
            set => Deleted = value ? DateTime.UtcNow : (DateTime?)null;
        }

        public Game()
        {
            Created = DateTime.Now;
        }

        public Game(SlpReplay slpReplay)
        {
            FileName = slpReplay.FileName;
            Hash = slpReplay.Hash;
            StartAt = slpReplay.MetaData.StartAt;
            StartingSeed = slpReplay.StartingSeed;
            GameLength = slpReplay.MetaData.LastFrame;
            Platform = slpReplay.MetaData.PlayedOn;

            Character1 = slpReplay.Settings.Players.Count > 0 ? slpReplay.Settings.Players[0]?.CharacterId : null;
            Character2 = slpReplay.Settings.Players.Count > 1 ? slpReplay.Settings.Players[1]?.CharacterId : null;
            Character3 = slpReplay.Settings.Players.Count > 2 ? slpReplay.Settings.Players[2]?.CharacterId : null;
            Character4 = slpReplay.Settings.Players.Count > 3 ? slpReplay.Settings.Players[3]?.CharacterId : null;

            Stage = slpReplay.Settings.StageId;
            GameMode = slpReplay.Settings.GameMode;
            GameEndMethod = slpReplay?.GameEnd?.GameEndMethod;

            bool?[] victories = SlpReplay.DetermineVictories(slpReplay);
            Player1Victory = victories[0];
            Player2Victory = victories[1];

            Created = DateTime.Now;
        }

        private Game(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            Player1Id = dataReader.GetValue<int?>(nameof(Player1Id));
            Player2Id = dataReader.GetValue<int?>(nameof(Player2Id));
            Player3Id = dataReader.GetValue<int?>(nameof(Player3Id));
            Player4Id = dataReader.GetValue<int?>(nameof(Player4Id));
            Character1 = (Character?)dataReader.GetValue<int?>(nameof(Character1));
            Character2 = (Character?)dataReader.GetValue<int?>(nameof(Character2));
            Character3 = (Character?)dataReader.GetValue<int?>(nameof(Character3));
            Character4 = (Character?)dataReader.GetValue<int?>(nameof(Character4));
            Player1Victory = dataReader.GetValue<bool?>(nameof(Player1Victory));
            Player2Victory = dataReader.GetValue<bool?>(nameof(Player2Victory));
            Player3Victory = dataReader.GetValue<bool?>(nameof(Player3Victory));
            Player4Victory = dataReader.GetValue<bool?>(nameof(Player4Victory));
            Stage = (Stage)dataReader.GetValue<int>(nameof(Stage));
            GameMode = (GameMode?)dataReader.GetValue<int?>(nameof(GameMode));
            GameEndMethod = (GameEndMethod?)dataReader.GetValue<int?>(nameof(GameEndMethod));
            StartAt = dataReader.GetValue<DateTime>(nameof(StartAt));
            StartingSeed = dataReader.GetValue<long?>(nameof(StartingSeed));
            GameLength = dataReader.GetValue<int>(nameof(GameLength));
            FileName = dataReader.GetValue<string>(nameof(FileName));
            Hash = dataReader.GetValue<string>(nameof(Hash));
            Platform = dataReader.GetValue<string>(nameof(Platform));
            Created = dataReader.GetValue<DateTime>(nameof(Created));
            Updated = dataReader.GetValue<DateTime?>(nameof(Updated));
            Deleted = dataReader.GetValue<DateTime?>(nameof(Deleted));
        }

        public static Game GetByHash(IDbConnection connection, string hash)
        {
            Game user = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetByHash)}",
                new { hash });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                user = new Game(reader);
            }

            return user;
        }

        public static List<Game> GetList(IDbConnection connection, bool includeAnonymous = false)
        {
            List<Game> games = new List<Game>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetList)}",
                new { includeAnonymous });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                games.Add(new Game(reader));
            }

            return games;
        }

        public static List<Game> GetListByPlayerId(IDbConnection connection, int playerId)
        {
            List<Game> games = new List<Game>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetListByPlayerId)}",
                new { playerId });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                games.Add(new Game(reader));
            }

            return games;
        }

        public static List<Game> GetListByPlayerIdFilters(IDbConnection connection, int playerId, Character? character = null, Character? opponentCharacter = null, Stage? stage = null, int? opponentPlayerId = null)
        {
            List<Game> games = new List<Game>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetListByPlayerIdFilters)}",
                new { playerId, character, opponentCharacter, stage, opponentPlayerId });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                games.Add(new Game(reader));
            }

            return games;
        }

        public static List<Game> GetListByFilters(IDbConnection connection, string playerName1 = null, string playerName2 = null, Character? character1 = null, Character? character2 = null, Stage? stage = null, bool includeAnonymous = false)
        {
            List<Game> games = new List<Game>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetListByFilters)}",
                new { playerName1, playerName2, character1, character2, stage, includeAnonymous });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                games.Add(new Game(reader));
            }

            return games;
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
                $"{nameof(Game)}_{nameof(Insert)}",
                new
                {
                    Character1,
                    Character2,
                    Character3,
                    Character4,
                    Player1Id,
                    Player2Id,
                    Player3Id,
                    Player4Id,
                    Player1Victory,
                    Player2Victory,
                    Player3Victory,
                    Player4Victory,
                    Stage,
                    GameMode,
                    GameEndMethod,
                    StartAt,
                    StartingSeed,
                    GameLength,
                    FileName,
                    Hash,
                    Platform,
                    Created,
                    Updated,
                    Deleted
                });

            Id = (int)command.ExecuteScalar();
        }

        private void Update(IDbConnection connection)
        {
            Updated = DateTime.UtcNow;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(Update)}",
                new
                {
                    Id,
                    Character1,
                    Character2,
                    Character3,
                    Character4,
                    Player1Id,
                    Player2Id,
                    Player3Id,
                    Player4Id,
                    Player1Victory,
                    Player2Victory,
                    Player3Victory,
                    Player4Victory,
                    Stage,
                    GameMode,
                    GameEndMethod,
                    StartAt,
                    StartingSeed,
                    GameLength,
                    FileName,
                    Hash,
                    Platform,
                    Created,
                    Updated,
                    Deleted
                });

            command.ExecuteNonQuery();
        }
    }
}
