using SlippiStats.Util;
using System;
using System.Data;

namespace SlippiStats.Models
{
    public class Game
    {
        public int Id { get; private set; }

        public string Hash { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public Character Character1 { get; set; }

        public Character Character2 { get; set; }

        public DateTime StartAt { get; set; }

        public int GameLength { get; set; }

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
            Created = DateTime.UtcNow;
        }

        public Game(SlpReplay slpReplay)
        {
            Hash = slpReplay.Hash;
            StartAt = slpReplay.MetaData.StartAt;
            GameLength = slpReplay.MetaData.LastFrame;
            Platform = slpReplay.MetaData.PlayedOn;

            Player1 = slpReplay.MetaData.Players["0"].Names.Netplay;
            Player2 = slpReplay.MetaData.Players["1"].Names.Netplay;

            Character1 = (Character)slpReplay.Settings.Players[0].CharacterId;
            Character2 = (Character)slpReplay.Settings.Players[1].CharacterId;
            Created = DateTime.UtcNow;
        }

        private Game(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            Hash = dataReader.GetValue<string>(nameof(Hash));
            Player1 = dataReader.GetValue<string>(nameof(Player1));
            Player2 = dataReader.GetValue<string>(nameof(Player2));
            Character1 = (Character)dataReader.GetValue<int>(nameof(Character1));
            Character2 = (Character)dataReader.GetValue<int>(nameof(Character2));
            StartAt = dataReader.GetValue<DateTime>(nameof(StartAt));
            GameLength = dataReader.GetValue<int>(nameof(GameLength));
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
                    Character1,
                    Character2,
                    StartAt,
                    GameLength,
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
                    Character1,
                    Character2,
                    StartAt,
                    GameLength,
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
