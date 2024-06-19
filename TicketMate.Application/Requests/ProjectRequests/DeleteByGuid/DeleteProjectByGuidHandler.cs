using MySql.Data.MySqlClient;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;

namespace TicketMate.Application.Requests.ProjectRequests.DeleteByGuid
{
    internal class DeleteProjectByGuidHandler : DataRequestHandler<DeleteProjectByGuidRequest>
    {
        public DeleteProjectByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public async override Task ExecuteRequestAsync(DeleteProjectByGuidRequest request)
        {
            var projectDTO = await _dataAccess.FetchAsync(new GetProjectByGuid(request.Guid));

            if (projectDTO == null)
            {
                throw new DoesNotExistException(nameof(Project), (request.Guid, nameof(request.Guid)));
            }
            
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(request.Guid));

            if (rowsAffected <= 0)
            {
                throw new OperationFailedException();
            }
        }
    }
}
