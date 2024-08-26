using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Persistence.Tests.DataRequestTests.Tickets
{
    /// <summary>
    /// Persistence Layer Tests for Getting a Ticket by GUID
    /// </summary>
    public class GetTicketByGuidTests : BaseDataRequestTest
    {
        [Fact]
        public async Task GetTicketByGuid_Given_TicketNotExisting_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetTicketByGuid(Guid.NewGuid())));
        }

        [Fact]
        public async Task GetTicketByGuid_Given_TicketExists_ShouldReturn_Ticket_DTO()
        {
            var projects_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var user_DTO = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticketGuid = Guid.NewGuid();

            await TestTicket.InsertTicketAsync(
                ticketGuid: ticketGuid, projectGuid: projects_DTO.Guid, userGuid: user_DTO.Guid);

            var result = await _dataAccess.FetchAsync(new GetTicketByGuid(ticketGuid));

            // Delete Inserted Results //
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticketGuid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(projects_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(user_DTO.Guid));

            Assert.NotNull(result);

        }
    }
}
