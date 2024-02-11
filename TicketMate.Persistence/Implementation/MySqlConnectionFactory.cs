using MySql.Data.MySqlClient;
using System.Data;

namespace TicketMate.Persistence.Implementation
{
    internal class MySqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public MySqlConnectionFactory(string connectionString) => _connectionString = connectionString;

        public MySqlConnectionFactory(string server, uint port, string databaseName, string userId, string password) 
        {
            var builder = new MySqlConnectionStringBuilder()
            {
                Server = server, // http://localhost:3309 from docker
                Database = databaseName, // Ticketmate
                UserID = userId, //ticketmate_dev_user
                Password = password, // ticketmate_dev_pw
                Port = port
            };

            _connectionString = builder.ConnectionString;
        }

        public IDbConnection NewConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
