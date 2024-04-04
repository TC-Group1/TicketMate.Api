using TicketMate.Persistence.Abstraction;
using TicketMate.Persistence.Implementation;

namespace TicketMate.Persistence.Tests.DataRequestTests
{
    public abstract class BaseDataRequestTest
    {
        protected readonly IDataAccess _dataAccess;

        public BaseDataRequestTest()
        {
            /*
                Hidden.cs class has been added to Git Ignore - You will need to add your own Hidden.cs to the TicketMate.Persistence.Tests project
                and add the properties for Hidden.cs since this class is not tracked in Git Source Control.
                
                Example:    
                            internal class Hidden
                            {
                                internal const string DbServer = "localhost";
                                internal const string DbName = "TicketMate";
                                internal const string DbUserId = "REPLACE THIS WITH DB USERNAME";
                                internal const string DbPassword = "REPLACE THIS WITH DB PASSWORD";
                                // Our Docker DB is exposed on port 3309
                                internal const string DbPort = "3309"; 
                            }
            */

            _dataAccess = new DataAccess(new MySqlConnectionFactory(Hidden.DbServer, Hidden.DbPort, Hidden.DbName, Hidden.DbUserId, Hidden.DbPassword));
        }
    }
}
