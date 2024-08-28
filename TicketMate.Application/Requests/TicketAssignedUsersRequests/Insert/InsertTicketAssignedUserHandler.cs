using TicketMate.Domain.Constants;
using TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest;

namespace TicketMate.Application.Requests.TicketAssignedUsersRequests.Insert
{
	internal class InsertTicketAssignedUserHandler : DataRequestHandler<InsertTicketAssignedUserRequest>
	{
		public InsertTicketAssignedUserHandler(IDataAccess dataAccess) : base(dataAccess)
		{
		}

		public async override Task ExecuteRequestAsync(InsertTicketAssignedUserRequest request)
		{
			try 
			{
				var rowsAffected = await _dataAccess.ExecuteAsync(new InsertTicketAssignedUser(
																			request.TicketGuid,
																			request.UserGuid));
			
			} 
			catch (DataAccessException ex)
			{
				if (ex.ExceptionNumber == MySqlExceptionNumber.ColumnCannotBeNull) 
				{
					throw new DoesNotExistException("User or Ticket", (request.TicketGuid, nameof(request.TicketGuid)),(request.UserGuid, nameof(request.UserGuid)));
				}

				throw;
			}
		}
	}
}
