using Moq;
using TicketMate.Application.Requests.TicketRequests.Insert;
using TicketMate.Application.Tests.Helpers;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;

namespace TicketMate.Application.Tests.RequestTests.TicketRequestTests.InsertTicketTests
{
    public class InsertTicketHandlerTests : HandlerTest
    {
        private readonly InsertTicketRequest _request = new();

        private readonly InsertTicketHandler _handler;

        public InsertTicketHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task InsertTicketHandler_Given_TicketIsInserted_ShouldReturn_TaskCompletedSuccessfully()
        {
            var rowsAffected = 1;

            SetupMockExecuteAsync<InsertTicket>(rowsAffected);

            var task = _handler.ExecuteRequestAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task InsertTicketHandler_Given_InsertTicketReturnsNoRowsAffected_ShouldReturn_OperationFailedException()
        {
            var rowsAffected = 0;

            SetupMockExecuteAsync<InsertTicket>(rowsAffected);

            var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<OperationFailedException>(exception);
        }

        [Fact]
        public async Task InsertTicketHandler_Given_MySqlException_DuplicateGuid_ShouldThrow_AlreadyExistsException()
        {
            var mySqlException = MySqlExceptionHelper.Instantiate("Duplicate entry 'fdfd5be4-ae90-49df-bdd1-37ef67ce4ec8' for key 'Tickets.Guid_UNIQUE'");

            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<InsertTicket>())).Throws(mySqlException);

            var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<AlreadyExistsException>(exception);
        }

        [Fact]
        public async Task InsertTicketHandler_Given_MySqlException_UnableToConnect_ShouldThrow_OperationFailedException()
        {
            var mySqlException = MySqlExceptionHelper.Instantiate("Unable to connect to any of the specified MySQL hosts.");

            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<InsertTicket>())).Throws(mySqlException);

            var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<OperationFailedException>(exception);
        }
    }
}
