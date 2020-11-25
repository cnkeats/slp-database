using SlippiStats.GameDataEnums;
using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace SlippiStats.Models
{
    public class PlayerAlias
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
            set => Deleted = value ? DateTime.UtcNow : (DateTime?)null;
        }

        public PlayerAlias()
        {
            Created = DateTime.Now;
        }

        private PlayerAlias(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            Name = dataReader.GetValue<string>(nameof(Name));
            ConnectCode = dataReader.GetValue<string>(nameof(ConnectCode));
            DiscordCode = dataReader.GetValue<string>(nameof(DiscordCode));
            Created = dataReader.GetValue<DateTime>(nameof(Created));
            Updated = dataReader.GetValue<DateTime?>(nameof(Updated));
            Deleted = dataReader.GetValue<DateTime?>(nameof(Deleted));
        }

        public static PlayerAlias GetByConnectCode(IDbConnection connection, string connectCode)
        {
            PlayerAlias player = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(PlayerAlias)}_{nameof(GetByConnectCode)}",
                new { connectCode });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                player = new PlayerAlias(reader);
            }

            return player;
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
                $"{nameof(PlayerAlias)}_{nameof(Insert)}",
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
            Updated = DateTime.UtcNow;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(PlayerAlias)}_{nameof(Update)}",
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
