using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace SlippiStats.Models
{
    public class User
    {
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
            Created = DateTime.UtcNow;
        }

        private User(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            UserName = dataReader.GetValue<string>(nameof(UserName));
            Password = dataReader.GetValue<string>(nameof(Password));
            Created = dataReader.GetValue<DateTime>(nameof(Created));
            Updated = dataReader.GetValue<DateTime?>(nameof(Updated));
            Deleted = dataReader.GetValue<DateTime?>(nameof(Deleted));
        }

        public int Id { get; private set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime Created { get; private set; }

        public DateTime? Updated { get; private set; }

        public DateTime? Deleted { get; private set; }

        public bool IsDeleted
        {
            get => Deleted != null;
            set => Deleted = value ? DateTime.UtcNow : (DateTime?)null;
        }

        public static User GetById(IDbConnection connection, int id)
        {
            User user = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(User)}_{nameof(GetById)}",
                new { id });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                user = new User(reader);
            }

            return user;
        }

        public static User GetByUserName(IDbConnection connection, string userName)
        {
            User user = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(User)}_{nameof(GetByUserName)}",
                new { userName });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                user = new User(reader);
            }

            return user;
        }

        public static List<User> GetList(IDbConnection connection)
        {
            List<User> users = new List<User>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(User)}_{nameof(GetList)}");

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User(reader));
            }

            return users;
        }

        public static List<User> GetListByFilters(IDbConnection connection, string userName)
        {
            List<User> users = new List<User>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(User)}_{nameof(GetListByFilters)}",
                new { userName });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User(reader));
            }

            return users;
        }

        public static List<User> GetListByRole(IDbConnection connection, string roleName)
        {
            List<User> users = new List<User>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(User)}_{nameof(GetListByRole)}",
                new { roleName });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User(reader));
            }

            return users;
        }

        public static bool IsUserNameInUse(IDbConnection connection, string userName)
        {
            return IsUserNameInUse(connection, userName, null);
        }

        public static bool IsUserNameInUse(IDbConnection connection, string userName, int? userId)
        {
            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(User)}_{nameof(IsUserNameInUse)}",
                new { userName, userId });

            return (bool)command.ExecuteScalar();
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
                $"{nameof(User)}_{nameof(Insert)}",
                new
                {
                    UserName,
                    Password,
                    Created,
                    Deleted
                });

            Id = (int)command.ExecuteScalar();
        }

        private void Update(IDbConnection connection)
        {
            Updated = DateTime.UtcNow;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(User)}_{nameof(Update)}",
                new
                {
                    Id,
                    UserName,
                    Password,
                    Updated,
                    Deleted
                });

            command.ExecuteNonQuery();
        }
    }
}