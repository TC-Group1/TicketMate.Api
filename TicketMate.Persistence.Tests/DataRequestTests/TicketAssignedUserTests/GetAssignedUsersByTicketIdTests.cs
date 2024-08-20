using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.Projects;
using TicketMate.Tests.Shared.Tickets;
using TicketMate.Tests.Shared.Users;

namespace TicketMate.Persistence.Tests.DataRequestTests.TicketAssignedUserTests
{
    public class GetAssignedUsersByTicketIdTests : BaseDataRequestTest
    {
        [Fact]
        public async Task GetAssignedUsersByTicketId_Given_ProjectTicketExists_ShouldReturn_ProjectUsers()
        {
            var project_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var existingUserOne = await TestUser.InsertAndFetchUsersDtoAsync();
            var existingUserTwo = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticket_DTO = await TestTicket.InsertAndFetchTicketDtoAsync(project_DTO.Guid, existingUserOne.Guid);

            await _dataAccess.ExecuteAsync(new InsertTicketAssignedUser(userId: existingUserOne.Id, ticketId: ticket_DTO.Id));
            await _dataAccess.ExecuteAsync(new InsertTicketAssignedUser(userId: existingUserTwo.Id, ticketId: ticket_DTO.Id));

            var results = await _dataAccess.FetchListAsync(new GetAssignedUsersByTicketId(ticket_DTO.Id));

            await _dataAccess.ExecuteAsync(new DeleteTicketAssignedUser(existingUserOne.Id, ticket_DTO.Id));
            await _dataAccess.ExecuteAsync(new DeleteTicketAssignedUser(existingUserTwo.Id, ticket_DTO.Id));
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticket_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(project_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUserOne.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUserTwo.Guid));

            var userOne = results.Single(_ => _.Id == existingUserOne.Id);
            var userTwo = results.Single(_ => _.Id == existingUserTwo.Id);


            Assert.Equal(existingUserOne.Guid, userOne.Guid);
            Assert.Equal(existingUserOne.PhoneNumber, userOne.PhoneNumber);
            Assert.Equal(existingUserOne.FirstName, userOne.FirstName);
            Assert.Equal(existingUserOne.LastName, userOne.LastName);
            Assert.Equal(existingUserOne.Email, userOne.Email);
            Assert.Equal(existingUserOne.Avatar, userOne.Avatar);
            Assert.Equal(existingUserOne.IsActive, userOne.IsActive);
            Assert.Equal(existingUserOne.PasswordHash, userOne.PasswordHash);

            Assert.Equal(existingUserTwo.Guid, userTwo.Guid);
            Assert.Equal(existingUserTwo.PhoneNumber, userTwo.PhoneNumber);
            Assert.Equal(existingUserTwo.FirstName, userTwo.FirstName);
            Assert.Equal(existingUserTwo.LastName, userTwo.LastName);
            Assert.Equal(existingUserTwo.Email, userTwo.Email);
            Assert.Equal(existingUserTwo.Avatar, userTwo.Avatar);
            Assert.Equal(existingUserTwo.IsActive, userTwo.IsActive);
            Assert.Equal(existingUserTwo.PasswordHash, userTwo.PasswordHash);
        }

        [Fact]
        public async Task GetAssignedUsersByTicket_Given_TicketNotExisting_ShouldReturn_Empty()
        {
            Assert.Empty(await _dataAccess.FetchListAsync(new GetAssignedUsersByTicketId(int.MinValue)));
        }
    }
}
