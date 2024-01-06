using System.Data;
using TicketMate.Persistence.Implementation;

namespace TicketMate.Persistence.Tests
{
    public class MySqlConnectionFactoryTests
    {
        [Fact]
        public void MySqlConnectionFactory_Given_ConnectionDetails_Should_ReturnConnection_ThatCanOpen()
        {
            var connectionFactory = new MySqlConnectionFactory(Hidden.DbServer, Hidden.DbName, Hidden.DbUserId, Hidden.DbPassword);

            using var connection = connectionFactory.NewConnection();

            connection.Open();

            Assert.Equal(ConnectionState.Open, connection.State);
        }

        [Fact]
        public void MySqlConnectionFactory_Given_ConnectionString_Should_ReturnConnection_ThatCanOpen()
        {
            var connectionString = $"server={Hidden.DbServer};uid={Hidden.DbUserId};pwd={Hidden.DbPassword};database={Hidden.DbName}";

            var connectionFactory = new MySqlConnectionFactory(connectionString);

            using var connection = connectionFactory.NewConnection();

            connection.Open();

            Assert.Equal(ConnectionState.Open, connection.State);
        }
    }
}
