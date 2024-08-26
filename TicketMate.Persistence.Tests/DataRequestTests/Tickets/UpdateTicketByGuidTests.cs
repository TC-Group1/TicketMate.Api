using TicketMate.Domain.Enums;
using TicketMate.Domain.Models;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.Helpers;
using TicketMate.Tests.Shared.Projects;
using TicketMate.Tests.Shared.Tickets;
using TicketMate.Tests.Shared.Users;

namespace TicketMate.Persistence.Tests.DataRequestTests.Tickets
{
    public class UpdateTicketByGuidTests : BaseDataRequestTest
    {
        [Fact]
        public async Task UpdateTicketByGuid_IfUpdateSuccessful_ShouldReturnOneRowAffected()
        {
            var projects_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var user_DTO = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticketGuid = Guid.NewGuid();

            await TestTicket.InsertTicketAsync(
                ticketGuid: ticketGuid, projectGuid: projects_DTO.Guid, userGuid: user_DTO.Guid);

            var updateRequest = new UpdateTicketByGuid(ticketGuid, TestString.Random(), TestString.Random(), null, Statuses.New);

            var rowsAffected = await _dataAccess.ExecuteAsync(updateRequest);

            Assert.Equal(1, rowsAffected);

            // Delete Inserted Results //
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticketGuid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(projects_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(user_DTO.Guid));
        }

        [Fact]
        public async Task UpdateTicketByGuid_IfUpdateSuccessful_ValuesAreUpdated()
        {
            var projects_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var user_DTO = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticketGuid = Guid.NewGuid();

            var insertTicketRequest = new InsertTicket(ticketGuid,
                               projects_DTO.Guid,
                               TestString.Random(),
                               TestString.Random(),
                               user_DTO.Guid);

            await _dataAccess.ExecuteAsync(insertTicketRequest);

            var updateRequest = new UpdateTicketByGuid(ticketGuid, TestString.Random(), TestString.Random(), null, Statuses.New);

            await _dataAccess.ExecuteAsync(updateRequest);
            var getTicket = await _dataAccess.FetchAsync(new GetTicketByGuid(ticketGuid));

            Assert.NotNull(getTicket);
            Assert.Equal(updateRequest.Title, getTicket.Title);
            Assert.Equal(insertTicketRequest.StatusId, getTicket.StatusId);
            Assert.Equal(insertTicketRequest.PriorityId, getTicket?.PriorityId);
            Assert.Equal(updateRequest.Description, getTicket?.Description);

            // Delete Inserted Results //
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticketGuid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(projects_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(user_DTO.Guid));
        }

        [Fact]
        public async Task UpdateTicketByGuid_IfTicketDoesNotExist_ShouldReturnZeroRowAffected()
        {

            var updateRequest = new UpdateTicketByGuid(Guid.NewGuid(), TestString.Random(), TestString.Random(), null, Statuses.New);

            var rowsAffected = await _dataAccess.ExecuteAsync(updateRequest);

            Assert.Equal(0, rowsAffected);
        }
    }
}
