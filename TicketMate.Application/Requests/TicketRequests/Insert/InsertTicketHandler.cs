using MySql.Data.MySqlClient;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;

namespace TicketMate.Application.Requests.TicketRequests.Insert
{
    internal class InsertTicketHandler : DataRequestHandler<InsertTicketRequest>
    {
        public InsertTicketHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public async override Task ExecuteRequestAsync(InsertTicketRequest request)
        {
            try
            {
                var rowsAffected = await _dataAccess.ExecuteAsync(
                    new InsertTicket(
                        request.Guid,
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
                    throw new AlreadyExistsException(nameof(Ticket), (request.Guid, nameof(request.Guid)));
                }

                throw new OperationFailedException();
            }
        }
    }
}
