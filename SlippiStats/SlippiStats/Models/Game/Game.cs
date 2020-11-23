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

        public int Character1 { get; set; }

        public int Character2 { get; set; }

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

            //Character1 = slpReplay.MetaData.Players["0"].Characters["1"];
            //Character2 = slpReplay.MetaData.Players["1"].Characters["0"];
            Created = DateTime.UtcNow;
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
                    Created,
                    Updated,
                    Deleted
                });

            command.ExecuteNonQuery();
        }
    }
}
