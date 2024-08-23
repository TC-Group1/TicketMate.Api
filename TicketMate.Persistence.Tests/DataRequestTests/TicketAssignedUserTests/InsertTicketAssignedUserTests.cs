using Org.BouncyCastle.Asn1.Ocsp;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Persistence.Tests.DataRequestTests.TicketAssignedUserTests
{
    public class InsertTicketAssignedUserTests : BaseDataRequestTest
    {
        [Fact]
        public async Task InsertTicketAssignedUser_Given_TicketAssignedUserIsInserted_Should_ReturnOneRowAffected()
        {
            var project_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticket_DTO = await TestTicket.InsertAndFetchTicketDtoAsync(project_DTO.Guid, existingUser.Guid);

            var insertTicketAssignedUserRequest = new InsertTicketAssignedUser(userId: existingUser.Id, ticketId: ticket_DTO.Id);

            var rowsAffected = await _dataAccess.ExecuteAsync(insertTicketAssignedUserRequest);
            
            await _dataAccess.ExecuteAsync(new DeleteTicketAssignedUser(insertTicketAssignedUserRequest.UserId, insertTicketAssignedUserRequest.TicketId));
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticket_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(project_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));
            
            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task InsertTicketAssignedUser_Given_UserDoesNotExist_ShouldThrow_DataAccessException()
        {
            var project_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticket_DTO = await TestTicket.InsertAndFetchTicketDtoAsync(project_DTO.Guid, existingUser.Guid);       

            var insertTicketAssignedUserRequest = new InsertTicketAssignedUser(userId: int.MinValue, ticketId: ticket_DTO.Id);

            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(insertTicketAssignedUserRequest));

            Assert.IsType<DataAccessException>(exception);

            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticket_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(project_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));
        }

        [Fact]
        public async Task InsertTicketAssignedUser_Given_TicketDoesNotExist_ShouldThrow_DataAccessException()
        {
            var project_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticket_DTO = await TestTicket.InsertAndFetchTicketDtoAsync(project_DTO.Guid, existingUser.Guid);

            var insertTicketAssignedUserRequest = new InsertTicketAssignedUser(userId: existingUser.Id, ticketId: int.MinValue);

            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(insertTicketAssignedUserRequest));

            Assert.IsType<DataAccessException>(exception);

            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticket_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(project_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));
        }
    }
}
