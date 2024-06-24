using TicketMate.Domain.Constants;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;

namespace TicketMate.Application.Requests.ProjectRequests.UpdateByGuid
{
    internal class UpdateProjectByGuidHandler : DataRequestHandler<UpdateProjectByGuidRequest>
    {
        public UpdateProjectByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task ExecuteRequestAsync(UpdateProjectByGuidRequest request)
        {
            try
            {
                var rowsAffected = await _dataAccess.ExecuteAsync(
                    new UpdateProjectByGuid(
                        request.Guid,
                        request.Name,
                        request.IsActive));

                if (rowsAffected <= 0)
                    throw new OperationFailedException();
            }
            catch (DataAccessException ex)
            {
                if (ex.ExceptionNumber == (MySqlExceptionNumber.DuplicateEntry))
                    throw new AlreadyExistsException(nameof(Project), (request.Guid, nameof(request.Guid)), 
                                                                      (request.Name, nameof(request.Name)));

                throw new OperationFailedException();
            }
        }
    }
}
