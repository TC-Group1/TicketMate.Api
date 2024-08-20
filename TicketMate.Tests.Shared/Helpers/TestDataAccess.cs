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

#if DEBUG

            SharedInstance = new DataAccess(new MySqlConnectionFactory(Hidden.DbServer, Hidden.DbPort, Hidden.DbName, Hidden.DbUserId, Hidden.DbPassword));

            /*
                Hidden.cs class has been added to Git Ignore.

                You will need to add your own Hidden.cs to the TicketMate.Tests.Shared project
            
                Add the properties for Hidden.cs since this class is not tracked in Git Source Control.
                
                Example:    
                            internal class Hidden
                            {
                                internal const string DbServer = "localhost";
                                internal const string DbName = "TicketMate";
                                internal const string DbUserId = "REPLACE THIS WITH DB USERNAME";
                                internal const string DbPassword = "REPLACE THIS WITH DB PASSWORD";
                                // Our Docker DB is exposed on port 3309
                                internal const uint DbPort = 3309;
                            }
            */

#else

            SharedInstance = new DataAccess(new MySqlConnectionFactory("123-tm.mysql.database.azure.com", 3306, "TicketMate", "admin_tm_user", "root_password1!"));

#endif
        }

        public static readonly IDataAccess SharedInstance;
    }
}
