using TicketMate.Application.Requests.UserRequests.DeleteByGuid;

namespace TicketMate.Application.Requests.TicketRequests.DeleteByGuid
{
    internal class DeleteTicketByGuidHandler : DataRequestHandler<DeleteTicketByGuidRequest>
    {
        public DeleteTicketByGuidHandler(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public override Task ExecuteRequestAsync(DeleteTicketByGuidRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
