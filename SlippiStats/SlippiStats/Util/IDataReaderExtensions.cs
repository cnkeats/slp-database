using System.Data;

namespace SlippiStats.Util
{
    public static class IDbConnectionExtensions
    {
        public static IDbCommand CreateStoredProcedure(this IDbConnection connection, string storedProcedureName)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedureName;

            return command;
        }

        public static IDbCommand CreateStoredProcedure(this IDbConnection connection, string storedProcedureName, object parameters)
        {
            IDbCommand command = connection.CreateStoredProcedure(storedProcedureName);
            command.AddParameters(parameters);

            return command;
        }
    }
}