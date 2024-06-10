using TicketMate.Application.Requests.UserRequests.UpdateByGuid;

namespace TicketMate.Application.Requests.TicketRequests.UpdateByGuid
{
    internal class UpdateTicketByGuidHandler : DataRequestHandler<UpdateTicketByGuidRequest>
    {
        public UpdateTicketByGuidHandler(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public override Task ExecuteRequestAsync(UpdateTicketByGuidRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
