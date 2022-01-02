using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SlippiStats.Models
{
    public class Tournament
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? TournamentOrganizerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }

        public bool IsDeleted
        {
            get => Deleted != null;
            set => Deleted = value ? DateTime.Now : (DateTime?)null;
        }

        public Tournament()
        {
            Created = DateTime.Now;
        }

        private Tournament(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            TournamentOrganizerId = dataReader.GetValue<int?>(nameof(TournamentOrganizerId));
            Name = dataReader.GetValue<string>(nameof(Name));
            StartDate = dataReader.GetValue<DateTime>(nameof(StartDate));
            EndDate = dataReader.GetValue<DateTime?>(nameof(EndDate));
            Created = dataReader.GetValue<DateTime>(nameof(Created));
            Updated = dataReader.GetValue<DateTime?>(nameof(Updated));
            Deleted = dataReader.GetValue<DateTime?>(nameof(Deleted));
        }

        public static Tournament GetById(IDbConnection connection, int id)
        {
            Tournament tournament = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Tournament)}_{nameof(GetById)}",
                new { id });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                tournament = new Tournament(reader);
            }

            return tournament;
        }

        public static Tournament GetByName(IDbConnection connection, string tournamentName)
        {
            Tournament tournament = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Tournament)}_{nameof(GetByName)}",
                new { tournamentName });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                tournament = new Tournament(reader);
            }

            return tournament;
        }

        public static List<Tournament> GetList(IDbConnection connection)
        {
            List<Tournament> tournaments = new List<Tournament>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Tournament)}_{nameof(GetList)}");

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                tournaments.Add(new Tournament(reader));
            }

            return tournaments;
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
                $"{nameof(Tournament)}_{nameof(Insert)}",
                new
                {
                    Name,
                    TournamentOrganizerId,
                    StartDate,
                    EndDate,
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
                $"{nameof(Tournament)}_{nameof(Update)}",
                new
                {
                    Id,
                    Name,
                    TournamentOrganizerId,
                    StartDate,
                    EndDate,
                    Created,
                    Updated,
                    Deleted
                });

            command.ExecuteNonQuery();
        }
    }
}
