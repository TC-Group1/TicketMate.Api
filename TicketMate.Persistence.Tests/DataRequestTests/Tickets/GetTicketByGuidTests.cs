using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Tests.Shared.Helpers;
using TicketMate.Tests.Shared.Projects;

namespace TicketMate.Persistence.Tests.DataRequestTests.Tickets
{
    public class GetTicketByGuidTests : BaseDataRequestTest
    {
        [Fact]
        public async Task GetUserByGuid_Given_UserNotExisting_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetTicketByGuid(Guid.NewGuid())));
        }

        [Fact]
        public async Task GetTicketByGuid_Given_TicketExists_ShouldReturn_Ticket_DTO()
        {
            var projects_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var ticketGuid = Guid.NewGuid();

            var insertTicketRequest = new InsertTicket(ticketGuid,
                               projects_DTO.Guid,
                               TestString.Random(),
                               TestString.Random(),
                               1,
                               1,
                               Guid.NewGuid());

            await _dataAccess.ExecuteAsync(insertTicketRequest);

            var result = await _dataAccess.FetchAsync(new GetTicketByGuid(ticketGuid));

            // Delete Inserted Results //
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticketGuid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(projects_DTO.Guid));

            Assert.NotNull(result);

        }
    }
}
