using MySql.Data.MySqlClient;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;

namespace TicketMate.Application.Requests.ProjectTicketRequests.Insert
{
    internal class InsertProjectTicketHandler : DataRequestHandler<InsertProjectTicketRequest>
    {
        public InsertProjectTicketHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public async override Task ExecuteRequestAsync(InsertProjectTicketRequest request)
        {
            try
            {
                var rowsAffected = await _dataAccess.ExecuteAsync(
                   new InsertTicket(
                       request.TicketGuid,
                       request.ProjectGuid,
                       request.Title,
                       request.Description,
                       request.CreatedByUserGuid,
                       request.PriorityId,
                       request.StatusId));

                if (rowsAffected <= 0)
                {
                    throw new OperationFailedException();
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Message.StartsWith("Duplicate entry") && ex.Message.EndsWith("'Tickets.Guid_UNIQUE'"))
                {
                    throw new AlreadyExistsException(nameof(Ticket), (request.TicketGuid, nameof(request.TicketGuid)));
                }

                else if (ex.Message.StartsWith("Duplicate entry") && ex.Message.EndsWith("'Projects.Guid_UNIQUE'"))
                {
                    throw new AlreadyExistsException(nameof(Project), (request.ProjectGuid, nameof(request.ProjectGuid)));
                }

                throw new OperationFailedException();
            }
        }
    }
}
