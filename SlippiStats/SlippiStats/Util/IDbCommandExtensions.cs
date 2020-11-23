using System;
using System.Data;
using System.Reflection;

namespace SlippiStats.Util
{
    public static class IDbCommandExtensions
    {
        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }

        public static void AddParameters(this IDbCommand command, object parameters)
        {
            foreach (PropertyInfo property in parameters.GetType().GetProperties())
            {
                command.AddParameter(property.Name, property.GetValue(parameters));
            }
        }
    }
}