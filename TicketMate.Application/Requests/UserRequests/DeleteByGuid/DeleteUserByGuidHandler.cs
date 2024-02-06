using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.DataRequestObjects.UserRolesRequests;

namespace TicketMate.Application.Requests.UserRequests.DeleteByGuid
{
    internal class DeleteUserByGuidHandler : DataRequestHandler<DeleteUserByGuidRequest>
    {
        public DeleteUserByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task ExecuteRequestAsync(DeleteUserByGuidRequest request)
        {
            var userDTO = await _dataAccess.FetchAsync(new GetUserByGuid(request.UserGuid));

            if (userDTO == null)
            {
                throw new DoesNotExistException(nameof(User), (request.UserGuid, nameof(request.UserGuid)));
            }

            var userRoles = await _dataAccess.FetchListAsync(new GetUserRolesByUserId(userDTO.Id));

            if (userRoles.Any())
            {
                var rolesDeleted = await _dataAccess.ExecuteAsync(new DeleteUserRolesByUserId(userDTO.Id));

                if (rolesDeleted < userRoles.Count())
                {
                    throw new OperationFailedException();
                }
            }

            var usersDeleted = await _dataAccess.ExecuteAsync(new DeleteUserByGuid(request.UserGuid));

            if(usersDeleted <= 0)
            {
                throw new OperationFailedException();
            }
        }
    }
}
