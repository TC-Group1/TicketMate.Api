using MySql.Data.MySqlClient;
using System.Data;

namespace TicketMate.Persistence.Implementation
{
    internal class MySqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public MySqlConnectionFactory(string connectionString) => _connectionString = connectionString;

        public MySqlConnectionFactory(string server, string databaseName, string userId, string password) 
        { 
            var builder = new MySqlConnectionStringBuilder()
            {
                Server = server,
                Database = databaseName,
                UserID = userId,
                Password = password,
            };

            _connectionString = builder.ConnectionString;
        }

        public IDbConnection NewConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
