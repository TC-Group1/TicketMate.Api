using MySql.Data.MySqlClient;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;

namespace TicketMate.Application.Requests.ProjectRequests.DeleteByGuid
{
    internal class DeleteProjectByGuidHandler : DataRequestHandler<DeleteProjectByGuidRequest>
    {
        public DeleteProjectByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public async override Task ExecuteRequestAsync(DeleteProjectByGuidRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(request.Guid));

            if (rowsAffected <= 0)
            {
                throw new OperationFailedException();
            }
        }
    }
}
