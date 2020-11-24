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

        public string Winner { get; set; }

        public Stage Stage { get; set; }

        public GameMode? GameMode { get; set; }

        public DateTime StartAt { get; set; }

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
            Winner = dataReader.GetValue<string>(nameof(Winner));
            Stage = (Stage)dataReader.GetValue<int>(nameof(Stage));
            GameMode = (GameMode?)dataReader.GetValue<int?>(nameof(GameMode));
            StartAt = dataReader.GetValue<DateTime>(nameof(StartAt));
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
                    Winner,
                    Stage,
                    GameMode,
                    StartAt,
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
                    Winner,
                    Stage,
                    GameMode,
                    StartAt,
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
