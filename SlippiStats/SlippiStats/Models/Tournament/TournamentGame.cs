using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SlippiStats.Models
{
    public class TournamentGame
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int TournamentId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }

        public bool IsDeleted
        {
            get => Deleted != null;
            set => Deleted = value ? DateTime.Now : (DateTime?)null;
        }

        public TournamentGame()
        {
            Created = DateTime.Now;
        }

        private TournamentGame(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            GameId = dataReader.GetValue<int>(nameof(GameId));
            TournamentId = dataReader.GetValue<int>(nameof(TournamentId));
            Created = dataReader.GetValue<DateTime>(nameof(Created));
            Updated = dataReader.GetValue<DateTime?>(nameof(Updated));
            Deleted = dataReader.GetValue<DateTime?>(nameof(Deleted));
        }

        public TournamentGame Unlink()
        {
            IsDeleted = true;
            return this;
        }

        public static TournamentGame GetById(IDbConnection connection, int id)
        {
            TournamentGame tournament = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(TournamentGame)}_{nameof(GetById)}",
                new { id });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                tournament = new TournamentGame(reader);
            }

            return tournament;
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
                $"{nameof(TournamentGame)}_{nameof(Insert)}",
                new
                {
                    GameId,
                    TournamentId,
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
                $"{nameof(TournamentGame)}_{nameof(Update)}",
                new
                {
                    Id,
                    GameId,
                    TournamentId,
                    Created,
                    Updated,
                    Deleted
                });

            command.ExecuteNonQuery();
        }
    }
}
