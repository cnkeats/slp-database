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

        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public string Player3 { get; set; }

        public string Player4 { get; set; }

        public Character? Character1 { get; set; }

        public Character? Character2 { get; set; }

        public Character? Character3 { get; set; }

        public Character? Character4 { get; set; }

        public int? Player1Id { get; set; }

        public int? Player2Id { get; set; }

        public int? Player3Id { get; set; }

        public int? Player4Id { get; set; }

        public string Winner { get; set; }

        public Stage Stage { get; set; }

        public GameMode? GameMode { get; set; }

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

            Player1 = Player.GetPlayerName(slpReplay, 0);
            Player2 = Player.GetPlayerName(slpReplay, 1);
            Player3 = Player.GetPlayerName(slpReplay, 2);
            Player4 = Player.GetPlayerName(slpReplay, 3);

            Character1 = slpReplay.Settings.Players.Count > 0 ? slpReplay.Settings.Players[0]?.CharacterId : null;
            Character2 = slpReplay.Settings.Players.Count > 1 ? slpReplay.Settings.Players[1]?.CharacterId : null;
            Character3 = slpReplay.Settings.Players.Count > 2 ? slpReplay.Settings.Players[2]?.CharacterId : null;
            Character4 = slpReplay.Settings.Players.Count > 3 ? slpReplay.Settings.Players[3]?.CharacterId : null;

            Stage = slpReplay.Settings.StageId;
            GameMode = slpReplay.Settings.GameMode;
            
            Winner = SlpReplay.DetermineWinner(slpReplay);

            Created = DateTime.Now;
        }

        private Game(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            Player1 = dataReader.GetValue<string>(nameof(Player1));
            Player2 = dataReader.GetValue<string>(nameof(Player2));
            Player3 = dataReader.GetValue<string>(nameof(Player3));
            Player4 = dataReader.GetValue<string>(nameof(Player4));
            Character1 = (Character?)dataReader.GetValue<int?>(nameof(Character1));
            Character2 = (Character?)dataReader.GetValue<int?>(nameof(Character2));
            Character3 = (Character?)dataReader.GetValue<int?>(nameof(Character3));
            Character4 = (Character?)dataReader.GetValue<int?>(nameof(Character4));
            Player1Id = dataReader.GetValue<int?>(nameof(Player1Id));
            Player2Id = dataReader.GetValue<int?>(nameof(Player2Id));
            Player3Id = dataReader.GetValue<int?>(nameof(Player3Id));
            Player4Id = dataReader.GetValue<int?>(nameof(Player4Id));
            Winner = dataReader.GetValue<string>(nameof(Winner));
            Stage = (Stage)dataReader.GetValue<int>(nameof(Stage));
            GameMode = (GameMode?)dataReader.GetValue<int?>(nameof(GameMode));
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

        public static Game GetDuplicateMatch(IDbConnection connection, Game game)
        {
            Game matchedGame = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetDuplicateMatch)}",
                new { game.Player1, game.Player2, game.Character1, game.Character2, game.Stage, game.StartingSeed, game.GameLength });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                matchedGame = new Game(reader);
            }

            return matchedGame;
        }

        public static List<Game> GetList(IDbConnection connection)
        {
            List<Game> games = new List<Game>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetList)}");

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                games.Add(new Game(reader));
            }

            return games;
        }

        public static List<Game> GetListByFilters(IDbConnection connection, string playerName1 = null, string playerName2 = null, string characterName1 = null, string characterName2 = null, string stageName = null)
        {
            List<Game> games = new List<Game>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetListByFilters)}",
                new { playerName1, playerName2, characterName1, characterName2, stageName });

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
                    Player1,
                    Player2,
                    Player3,
                    Player4,
                    Character1,
                    Character2,
                    Character3,
                    Character4,
                    Player1Id,
                    Player2Id,
                    Player3Id,
                    Player4Id,
                    Winner,
                    Stage,
                    GameMode,
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
                    Player1,
                    Player2,
                    Player3,
                    Player4,
                    Character1,
                    Character2,
                    Character3,
                    Character4,
                    Player1Id,
                    Player2Id,
                    Player3Id,
                    Player4Id,
                    Winner,
                    Stage,
                    GameMode,
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
