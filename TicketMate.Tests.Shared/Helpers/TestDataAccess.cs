using System.Diagnostics.CodeAnalysis;
using TicketMate.Persistence.Abstraction;
using TicketMate.Persistence.Implementation;

namespace TicketMate.Tests.Shared.Helpers
{
    /// <summary>
    /// Allows you to Access the DB within the Test Helpers
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class TestDataAccess
    {
        static TestDataAccess()
        {
            SharedInstance = new DataAccess(new MySqlConnectionFactory(Hidden.DbServer, Hidden.DbPort, Hidden.DbName, Hidden.DbUserId, Hidden.DbPassword));
        }

        public static readonly IDataAccess SharedInstance;
    }
}
