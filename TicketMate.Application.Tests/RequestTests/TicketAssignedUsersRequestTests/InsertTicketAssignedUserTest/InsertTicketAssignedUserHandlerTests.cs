using Moq;
using TicketMate.Application.Requests.TicketAssignedUsersRequests.Insert;
using TicketMate.Domain.Constants;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest;

namespace TicketMate.Application.Tests.RequestTests.TicketAssignedUsersRequestTests.InsertTicketAssignedUserTest
{
	public class InsertTicketAssignedUserHandlerTests : HandlerTest
	{
		private readonly InsertTicketAssignedUserRequest _request = new();
		private readonly InsertTicketAssignedUserHandler _handler;

		public InsertTicketAssignedUserHandlerTests() => _handler = new(_mockDataAccess.Object);

		[Fact]
		public async Task InsertTicketAssignedUserHandler_Given_UserIsInserted_ShouldReturn_TaskCompletedSuccesfully()
		{
			var rowsAffected = 1;

			SetupMockExecuteAsync<InsertTicketAssignedUser>(rowsAffected);

			var task = _handler.ExecuteRequestAsync(_request);

			await task;

			Assert.True(task.IsCompletedSuccessfully);
		}

		[Fact]
		public async Task InsertTicketAssignedUser_Given_DataAccessException_ExceptionNumber_ColumnCannotBeNull_ShouldThrow_DoesNotExistException()
		{
			var dataAccessException = new DataAccessException() { ExceptionNumber = MySqlExceptionNumber.ColumnCannotBeNull };

			_mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<InsertTicketAssignedUser>())).Throws(dataAccessException);

			var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

			Assert.IsType<DoesNotExistException>(exception);
		}

		[Fact]
		public async Task InsertTicketAssignedUser_Given_DataAccessException_UnexpectedExceptionNumber_ShouldThrow_DataAccessException()
		{
			var expectedException = new DataAccessException();

			_mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<InsertTicketAssignedUser>())).Throws(expectedException);

			var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

			Assert.Equal(expectedException, exception);
		}
	}
}
