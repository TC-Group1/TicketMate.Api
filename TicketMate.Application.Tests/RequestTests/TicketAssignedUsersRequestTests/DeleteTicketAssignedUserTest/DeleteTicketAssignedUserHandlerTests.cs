using TicketMate.Application.Requests.TicketAssignedUsersRequests.Delete;
using TicketMate.Domain.Exceptions;
using TicketMate.Domain.Models;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.DataTransferObjects;

namespace TicketMate.Application.Tests.RequestTests.TicketAssignedUsersRequestTests.DeleteTicketAssignedUserTest
{
	public class DeleteTicketAssignedUserHandlerTests : HandlerTest
	{
		private readonly DeleteTicketAssignedUserRequest _request = new();

		private readonly DeleteTicketAssignedUserHandler _handler;

		public DeleteTicketAssignedUserHandlerTests() => _handler = new(_mockDataAccess.Object);


		[Fact]
		public async Task DeleteTicketAssignedUser_Given_TicketAssignedUserIsDeleted_ShouldReturn_TaskCompletedSuccessfully()
		{

			var rowsAffected = 1;

            SetupMockExecuteAsync<DeleteTicketAssignedUser>(rowsAffected);

			var task = _handler.HandleAsync(_request);

			await task;

			Assert.True(task.IsCompletedSuccessfully);
		}


		[Fact]
		public async Task DeleteTicketAssignedUser_Given_TicketDoesNotExist_ShouldThrow_DoesNotExistException()
		{
			var expectedNameOfObjectNotExisting = nameof(Ticket);

			var rowsAffected = 0;

            SetupMockExecuteAsync<DeleteTicketAssignedUser>(rowsAffected);

			SetupMockFetchAsync<GetTicketByGuid, Tickets_DTO>(null);

			var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

			Assert.IsType<DoesNotExistException>(exception);
			Assert.Equal(expectedNameOfObjectNotExisting, (exception as DoesNotExistException)!.NameOfObjectNotExisting);
		}

		[Fact]
		public async Task DeleteTicketAssignedUser_Given_UserDoesNotExist_ShouldThrow_DoesNotExistException()
		{
			var expectedNameOfObjectNotExisting = nameof(User);

			var rowsAffected = 0;

            SetupMockExecuteAsync<DeleteTicketAssignedUser>(rowsAffected);

			SetupMockFetchAsync<GetTicketByGuid, Tickets_DTO>(new Tickets_DTO());

			SetupMockFetchAsync<GetUserByGuid, Users_DTO>(null);

			var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

			Assert.IsType<DoesNotExistException>(exception);
			Assert.Equal(expectedNameOfObjectNotExisting, (exception as DoesNotExistException)!.NameOfObjectNotExisting);
		}
	}
}
