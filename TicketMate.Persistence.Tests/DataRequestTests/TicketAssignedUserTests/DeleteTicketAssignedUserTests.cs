using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Persistence.Tests.DataRequestTests.TicketAssignedUserTests
{
    public class DeleteTicketAssignedUserTests : BaseDataRequestTest
    {

        [Fact]
        public async Task DeleteTicketAssignedUser_Given_TicketAssignedUserIsDeleted_ShouldReturn_OneRowAffected()
        {
            var project_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticket_DTO = await TestTicket.InsertAndFetchTicketDtoAsync(project_DTO.Guid, existingUser.Guid);

            var insertTicketAssignedUserRequest = new InsertTicketAssignedUser( existingUser.Guid, ticket_DTO.Guid);

            await _dataAccess.ExecuteAsync(insertTicketAssignedUserRequest);

            var deleteTicketAssignedUser = new DeleteTicketAssignedUser(existingUser.Guid, ticket_DTO.Guid);

            var rowsAffected = await _dataAccess.ExecuteAsync(deleteTicketAssignedUser);

            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticket_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(project_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task DeleteTicketAssignedUser_Given_TicketDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var project_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticket_DTO = await TestTicket.InsertAndFetchTicketDtoAsync(project_DTO.Guid, existingUser.Guid);

            var insertTicketAssignedUserRequest = new InsertTicketAssignedUser(existingUser.Guid, ticket_DTO.Guid);

            await _dataAccess.ExecuteAsync(insertTicketAssignedUserRequest);

            var deleteTicketAssignedUser = new DeleteTicketAssignedUser(existingUser.Guid, Guid.NewGuid());

            var rowsAffected = await _dataAccess.ExecuteAsync(deleteTicketAssignedUser);

            await _dataAccess.ExecuteAsync(new DeleteTicketAssignedUser(existingUser.Guid, ticket_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticket_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(project_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));

            Assert.Equal(0, rowsAffected);
        }

        [Fact]
        public async Task DeleteTicketAssignedUser_Given_UserDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var project_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticket_DTO = await TestTicket.InsertAndFetchTicketDtoAsync(project_DTO.Guid, existingUser.Guid);

            var insertTicketAssignedUserRequest = new InsertTicketAssignedUser(existingUser.Guid, ticket_DTO.Guid);

            await _dataAccess.ExecuteAsync(insertTicketAssignedUserRequest);

            var deleteTicketAssignedUser = new DeleteTicketAssignedUser(Guid.NewGuid(), ticket_DTO.Guid);

            var rowsAffected = await _dataAccess.ExecuteAsync(deleteTicketAssignedUser);

            await _dataAccess.ExecuteAsync(new DeleteTicketAssignedUser(existingUser.Guid, ticket_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticket_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(project_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));

            Assert.Equal(0, rowsAffected);
        }
    }
}
