using TicketMate.Persistence.DataRequestObjects.TicketRequests;

namespace TicketMate.Application.Requests.TicketRequests.DeleteByGuid
{
    internal class DeleteTicketByGuidHandler : DataRequestHandler<DeleteProjectTicketRequest>
    {
        public DeleteTicketByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task ExecuteRequestAsync(DeleteProjectTicketRequest request)
        {
            var ticketDTO = await _dataAccess.FetchAsync(new GetTicketByGuid(request.TicketGuid));

            if (ticketDTO == null)
                throw new DoesNotExistException(nameof(Ticket), (request.TicketGuid, nameof(request.TicketGuid)));

            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(request.TicketGuid));

            if (rowsAffected <= 0)
                throw new OperationFailedException();
        }
    }
}
