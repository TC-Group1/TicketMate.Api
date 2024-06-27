using TicketMate.Application.Requests.TicketRequests.DeleteByGuid;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataTransferObjects;

namespace TicketMate.Application.Tests.RequestTests.TicketRequestTests.DeleteTicketTests
{
    public class DeleteTicketByGuidHandlerTests : HandlerTest
    {
        private readonly DeleteTicketByGuidRequest _request = new();

        private readonly DeleteTicketByGuidHandler _handler;

        public DeleteTicketByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteTicketByGuid_Given_TicketNotFoundWithGuid_ShouldThrow_DoesNotExistException()
        {
            SetupMockFetchAsync<GetTicketByGuid, Tickets_DTO>(null);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task DeleteTicketByGuid_Given_TicketFoundWithGuid_AndTicketIsDeleted_ShouldReturn_TaskCompletedSuccessfully()
        {
            SetupMockFetchAsync<GetTicketByGuid, Tickets_DTO>(new Tickets_DTO());

            SetupMockExecuteAsync<DeleteTicketByGuid>(1);

            var task = _handler.HandleAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }
    }
}
