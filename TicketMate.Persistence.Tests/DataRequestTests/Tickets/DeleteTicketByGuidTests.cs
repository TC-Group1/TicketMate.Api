using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Persistence.Tests.DataRequestTests.Tickets
{
    public class DeleteTicketByGuidTests : BaseDataRequestTest
    {
        [Fact]
        public async Task DeleteTicketByGuid_Given_TicketIsNotDeleted_ShouldReturn_ZeroRowsAffected()
        {
            Assert.Equal(0, await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(Guid.NewGuid())));
        }

        [Fact]
        public async Task DeleteTicketByGuid_Given_TicketIsDeleted_ShouldReturn_OneRowAffected()
        {
            var projects_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var user_DTO = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticketGuid = Guid.NewGuid();

            await TestTicket.InsertTicketAsync(
                ticketGuid: ticketGuid, projectGuid: projects_DTO.Guid, userGuid: user_DTO.Guid);

            // Get Ticket by Guid to Ensure it was Inserted //
            var ticketBeforeDeleting = await _dataAccess.FetchAsync(new GetTicketByGuid(ticketGuid));

            // Delete Ticket and Rows Affected //
            var rowsAffectedWhenDeletingTicket = await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticketGuid));

            var ticketAfterDeleting = await _dataAccess.FetchAsync(new GetTicketByGuid(ticketGuid));

            Assert.NotNull(ticketBeforeDeleting);

            Assert.Equal(1, rowsAffectedWhenDeletingTicket);

            Assert.Null(ticketAfterDeleting);
        }
    }
}
