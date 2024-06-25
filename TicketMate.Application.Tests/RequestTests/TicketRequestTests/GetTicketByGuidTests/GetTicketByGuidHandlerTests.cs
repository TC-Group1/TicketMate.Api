using GenFu;
using TicketMate.Application.Requests.TicketRequests.GetByGuid;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataTransferObjects;

namespace TicketMate.Application.Tests.RequestTests.TicketRequestTests.GetTicketByGuidTests
{
    public class GetTicketByGuidHandlerTests : HandlerTest
    {
        private readonly GetTicketByGuidRequest _request = new();

        private readonly GetTicketByGuidHandler _handler;

        public GetTicketByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetTicketByGuid_Given_TicketNotFoundWithGuid_ShouldThrow_DoesNotExistException()
        {
            SetupMockFetchAsync<GetTicketByGuid, Tickets_DTO>(null);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task GetTicketByGuid_Given_TicketFoundWithGuid_ShouldReturn_ExpectedTicket()
        {
            var ticketDto = A.New<Tickets_DTO>();

            var expected = ticketDto.AsDomainTicket();

            SetupMockFetchAsync<GetTicketByGuid, Tickets_DTO>(ticketDto);

            var response = await _handler.HandleAsync(_request) as GetTicketByGuidResponse;

            Assert.NotNull(response);
            Assert.Equivalent(expected, response.Ticket);
        }
    }
}
