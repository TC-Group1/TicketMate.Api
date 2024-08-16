using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;

namespace TicketMate.Application.Requests.ProjectTicketRequests.Delete
{
    internal class DeleteProjectTicketHandler : DataRequestHandler<DeleteProjectTicketRequest>
    {
        public DeleteProjectTicketHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public async override Task ExecuteRequestAsync(DeleteProjectTicketRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteProjectTicket(request.TicketGuid, request.ProjectGuid));

            if (rowsAffected <= 0)
            {
                var ticket = await _dataAccess.FetchAsync(new GetTicketByGuid(request.TicketGuid));

                if (ticket == null)
                {
                    throw new DoesNotExistException(nameof(Ticket),(request.TicketGuid, nameof(request.TicketGuid)));
                }

                var project = await _dataAccess.FetchAsync(new GetProjectByGuid(request.ProjectGuid));

                if (project == null)
                {
                    throw new DoesNotExistException(nameof(Project), (request.ProjectGuid, nameof(request.ProjectGuid)));
                }

                throw new OperationFailedException();
            }
        }
    }
}
