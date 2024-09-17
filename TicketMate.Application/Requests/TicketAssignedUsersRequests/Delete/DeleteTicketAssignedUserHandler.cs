using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest;
using TicketMate.Persistence.DataRequestObjects.UserRequests;

namespace TicketMate.Application.Requests.TicketAssignedUsersRequests.Delete
{
	internal class DeleteTicketAssignedUserHandler : DataRequestHandler<DeleteTicketAssignedUserRequest>
	{
		public DeleteTicketAssignedUserHandler(IDataAccess dataAccess) : base(dataAccess)
		{
		}

		public async override Task ExecuteRequestAsync(DeleteTicketAssignedUserRequest request)
		{
			var assignedUsersDeletedDeleted = await _dataAccess.ExecuteAsync(new Persistence.DataRequestObjects.TicketsAssignedUsersRequest.DeleteTicketAssignedUser(request.UserGuid, request.TicketGuid));
			if (assignedUsersDeletedDeleted <= 0)
			{
				var ticket = await _dataAccess.FetchAsync(new GetTicketByGuid(request.TicketGuid));


				if (ticket == null)
				{
					throw new DoesNotExistException(nameof(Ticket), (request.TicketGuid, nameof(request.TicketGuid)));
				}

				var user = await _dataAccess.FetchAsync(new GetUserByGuid(request.UserGuid));

				if (user == null)
				{
					throw new DoesNotExistException(nameof(User), (request.UserGuid, nameof(request.UserGuid)));
				}

				throw new OperationFailedException();
			}
		}
	}
}
