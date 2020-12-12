using SlippiStats.Util;
using System;
using System.Data;

namespace SlippiStats.Models
{
    public class UserSession
    {
        public UserSession(int userId, TimeSpan duration)
        {
            Created = DateTime.UtcNow;
            Expires = Created.Add(duration);
            UserId = userId;
        }

        private UserSession(IDataReader dataReader)
        {
            Id = dataReader.GetValue<Guid>(nameof(Id));
            Created = dataReader.GetValue<DateTime>(nameof(Created));
            Updated = dataReader.GetValue<DateTime?>(nameof(Updated));
            Deleted = dataReader.GetValue<DateTime?>(nameof(Deleted));
            Expires = dataReader.GetValue<DateTime>(nameof(Expires));
            UserId = dataReader.GetValue<int>(nameof(UserId));
        }

        public Guid Id { get; private set; }

        public DateTime Created { get; private set; }

        public DateTime? Updated { get; private set; }

        public DateTime? Deleted { get; private set; }

        public DateTime Expires { get; set; }

        public int UserId { get; private set; }

        public bool IsDeleted
        {
            get => Deleted != null;
            set => Deleted = value ? DateTime.UtcNow : (DateTime?)null;
        }

        public bool IsExpired => Expires <= DateTime.UtcNow;

        public static UserSession GetById(IDbConnection connection, Guid id)
        {
            UserSession userSession = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(UserSession)}_{nameof(GetById)}",
                new { id });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                userSession = new UserSession(reader);
            }

            return userSession;
        }

        public static UserSession GetLatestByUserId(IDbConnection connection, int userId)
        {
            UserSession userSession = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(UserSession)}_{nameof(GetLatestByUserId)}",
                new { userId });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                userSession = new UserSession(reader);
            }

            return userSession;
        }

        public void Save(IDbConnection connection)
        {
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
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
                $"{nameof(UserSession)}_{nameof(Insert)}",
                new
                {
                    Id,
                    Created,
                    Deleted,
                    Expires,
                    UserId
                });

            command.ExecuteNonQuery();
        }

        private void Update(IDbConnection connection)
        {
            Updated = DateTime.UtcNow;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(UserSession)}_{nameof(Update)}",
                new
                {
                    Id,
                    Updated,
                    Deleted,
                    Expires
                });

            command.ExecuteNonQuery();
        }
    }
}