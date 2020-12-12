using SlippiStats.Util;
using System.Collections.Generic;
using System.Data;

namespace SlippiStats.Models
{
    public class Role
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int DisplayOrder { get; private set; }

        private Role(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            Name = dataReader.GetValue<string>(nameof(Name));
            DisplayOrder = dataReader.GetValue<int>(nameof(DisplayOrder));
        }

        public static Role GetByUserId(IDbConnection connection, int userId)
        {
            Role role = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Role)}_{nameof(GetByUserId)}",
                new { userId });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                role = new Role(reader);
            }

            return role;
        }

        public static List<Role> GetList(IDbConnection connection)
        {
            List<Role> roles = new List<Role>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Role)}_{nameof(GetList)}");

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                roles.Add(new Role(reader));
            }

            return roles;
        }
    }
}