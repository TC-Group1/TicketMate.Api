using TicketMate.Persistence.Abstraction;
using TicketMate.Tests.Shared.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests
{
    public abstract class BaseDataRequestTest
    {
        protected IDataAccess _dataAccess => TestDataAccess.SharedInstance;
    }
}
