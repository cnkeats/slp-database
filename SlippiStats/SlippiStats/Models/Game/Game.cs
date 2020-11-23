using SlippiStats.Util;
using System;
using System.Data;

namespace SlippiStats.Models
{
    public class Game
    {
        public int Id { get; private set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public int GameLength { get; set; }

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
                    //SlpVersion,
                    //Player1Name,
                    //Player2Name,
                    Player1,
                    Player2,
                    GameLength,
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
                    //SlpVersion,
                    //Player1Name,
                    //Player2Name,
                    Player1,
                    Player2,
                    GameLength,
                    Created,
                    Updated,
                    Deleted
                });

            command.ExecuteNonQuery();
        }
    }
}
