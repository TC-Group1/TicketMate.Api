using TicketMate.Domain.Constants;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;

namespace TicketMate.Application.Requests.TicketRequests.UpdateByGuid
{
    internal class UpdateTicketByGuidHandler : DataRequestHandler<UpdateTicketByGuidRequest>
    {
        public UpdateTicketByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }
        public override async Task ExecuteRequestAsync(UpdateTicketByGuidRequest request)
        {
            try
            {
                var rowsAffected = await _dataAccess.ExecuteAsync(
                    new UpdateTicketByGuid(
                        request.Guid,
                        request.Title,
                        request.Description,
                        request.PriorityId,
                        request.StatusId));

                if (rowsAffected <= 0)
                    throw new OperationFailedException();
            }
            catch (DataAccessException ex)
            {
                if (ex.ExceptionNumber == (MySqlExceptionNumber.DuplicateEntry))
                    throw new AlreadyExistsException(nameof(Ticket), (request.Guid, nameof(request.Guid)) ,(request.Title, nameof(request.Title)));

                throw new OperationFailedException();
            }
        }
    }
}
