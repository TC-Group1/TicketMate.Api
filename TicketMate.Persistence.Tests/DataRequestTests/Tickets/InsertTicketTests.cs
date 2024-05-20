using MySql.Data.MySqlClient;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.Helpers;
using TicketMate.Tests.Shared.Projects;
using TicketMate.Tests.Shared.Users;

namespace TicketMate.Persistence.Tests.DataRequestTests.Tickets
{
    /// <summary>
    /// Persistence Layer Tests for Inserting a Ticket
    /// </summary>
    public class InsertTicketTests : BaseDataRequestTest
    {
        [Fact]
        public async Task InsertTicket_Given_TicketIsInserted_Should_ReturnOneRowAffected()
        {
            var projects_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var user_DTO = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticketGuid = Guid.NewGuid();

            var insertTicketRequest = new InsertTicket(ticketGuid,
                               projects_DTO.Guid,
                               TestString.Random(),
                               TestString.Random(),
                               null,
                               user_DTO.Guid);

            var rowsAffected = await _dataAccess.ExecuteAsync(insertTicketRequest);

            // Delete Inserted Results //
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticketGuid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(projects_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(user_DTO.Guid));

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task InsertTicket_Given_GuidAlreadyTaken_ShouldThrow_MySqlException()
        {
            var projects_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

            var user_DTO = await TestUser.InsertAndFetchUsersDtoAsync();

            var ticketGuid = Guid.NewGuid();

            await _dataAccess.ExecuteAsync(new InsertTicket(ticketGuid,
                               projects_DTO.Guid,
                               TestString.Random(),
                               TestString.Random(),
                               null,
                               user_DTO.Guid));

            // Create request with guid that was just inserted
            var request = new InsertTicket(ticketGuid,
                               projects_DTO.Guid,
                               TestString.Random(),
                               TestString.Random(),
                               null,
                               user_DTO.Guid);

            // assert that request throws MySqlException
            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            Assert.IsType<DataAccessException>(exception);

            // Delete Inserted Results //
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(ticketGuid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(projects_DTO.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(user_DTO.Guid));
        }
    }
}
