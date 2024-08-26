using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Persistence.Tests.DataRequestTests.ProjectTicketTests
{
    public class InsertProjectTicketTests : BaseDataRequestTest
    {
        [Fact]
        public async Task InsertProjectTicket_Given_ProjectTicketInserted_ShouldReturn_OneRowAffected()
        {
            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();
            var existingProject = await TestProject.InsertAndFetchProjectDtoAsync();
            var existingTicket = await TestTicket.InsertAndFetchTicketDtoAsync(existingProject.Guid, existingUser.Guid);

            var request = new InsertProjectTicket(existingProject.Id, existingTicket.Id);

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            await _dataAccess.ExecuteAsync(new DeleteProjectTicket(existingProject.Id, existingTicket.Id));
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(existingTicket.Guid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(existingProject.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task InsertProjectTicket_Given_ProjectNotExisting_ShouldThrow_DataAccessException()
        {
            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();
            var existingProject = await TestProject.InsertAndFetchProjectDtoAsync();
            var existingTicket = await TestTicket.InsertAndFetchTicketDtoAsync(existingProject.Guid, existingUser.Guid);

            var request = new InsertProjectTicket(int.MinValue, existingTicket.Id);

            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(existingTicket.Guid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(existingProject.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));

            Assert.IsType<DataAccessException>(exception);
        }

        [Fact]
        public async Task InsertProjectTicket_Given_TicketNotExisting_ShouldThrow_DataAccessException()
        {
            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();
            var existingProject = await TestProject.InsertAndFetchProjectDtoAsync();

            var request = new InsertProjectTicket(existingProject.Id, int.MinValue);

            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(existingProject.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));

            Assert.IsType<DataAccessException>(exception);
        }
    }
}
