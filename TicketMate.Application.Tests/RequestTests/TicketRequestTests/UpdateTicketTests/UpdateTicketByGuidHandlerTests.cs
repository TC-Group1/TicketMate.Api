using Moq;
using TicketMate.Application.Requests.TicketRequests.UpdateByGuid;
using TicketMate.Domain.Constants;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Application.Tests.RequestTests.TicketRequestTests.UpdateTicketTests
{
    public class UpdateTicketByGuidHandlerTests : HandlerTest
    {
        private readonly UpdateTicketByGuidRequest _request = new();

        private readonly UpdateTicketByGuidHandler _handler;

        public UpdateTicketByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task UpdateTicketHandler_Given_TicketIsUpdated_ShouldReturn_TaskCompletedSuccessfully()
        {
            var rowsAffected = 1;

            SetupMockExecuteAsync<UpdateTicketByGuid>(rowsAffected);

            var task = _handler.ExecuteRequestAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task UpdateTicketHandler_Given_UpdateTicketReturnsNoRowsAffected_ShouldReturn_OperationFailedException()
        {
            var rowsAffected = 0;

            SetupMockExecuteAsync<UpdateTicketByGuid>(rowsAffected);

            var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<OperationFailedException>(exception);
        }

        [Fact]
        public async Task UpdateTicketByGuid_Given_DuplicateEntry_ShouldThrow_AlreadyExistsException()
        {
            var guid = Guid.NewGuid();
            var request = new UpdateTicketByGuidRequest
            {
                Guid = guid,
                Title = TestString.Random(25)
            };

            var ex = new DataAccessException("Duplicate entry 'Ticket for key 'Tickets", MySqlExceptionNumber.DuplicateEntry, null!);

            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<UpdateTicketByGuid>())).Throws(ex);

            var result = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(request));

            Assert.IsType<AlreadyExistsException>(result);
        }
    }
}
