using SlippiStats.Util;
using System.Collections.Generic;
using System.Data;

namespace SlippiStats.Models
{
    public class UserRole
    {
        private UserRole(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            UserId = dataReader.GetValue<int>(nameof(UserId));
            RoleId = dataReader.GetValue<int>(nameof(RoleId));
            RoleName = dataReader.GetValue<string>(nameof(RoleName));
        }

        public int Id { get; private set; }

        public int UserId { get; private set; }

        public int RoleId { get; private set; }

        public string RoleName { get; private set; }

        public static List<UserRole> GetListByUserId(IDbConnection connection, int userId)
        {
            List<UserRole> userRoles = new List<UserRole>();

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(UserRole)}_{nameof(GetListByUserId)}",
                new { userId });

            using IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                userRoles.Add(new UserRole(reader));
            }

            return userRoles;
        }
    }
}