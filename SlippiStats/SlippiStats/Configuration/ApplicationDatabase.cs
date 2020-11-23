using System;
using System.Data;
using System.Data.SqlClient;

namespace SlippiStats.Configuration
{
    public class ApplicationDatabase : IDisposable
    {
        public ApplicationDatabase(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        ~ApplicationDatabase()
        {
            Dispose(false);
        }

        public IDbConnection Connection { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                Connection?.Dispose();
                Connection = null;
            }
        }
    }
}