using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Persistence.Tests.DataRequestTests.ProjectTicketTests
{
    public class GetProjectTicketTests : BaseDataRequestTest
    {
        [Fact]
        public async Task GetProjectTicket_Given_ProjectTicketNotExisting_ShouldReturn_Empty()
        {
            Assert.Empty(await _dataAccess.FetchListAsync(new GetProjectTickets(int.MinValue)));
        }

        [Fact]
        public async Task GetProjectTicket_Given_ProjectTicketExist_ShouldReturn_ProjectTickets()
        {
            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();
            var existingProject = await TestProject.InsertAndFetchProjectDtoAsync();
            var existingTicketOne = await TestTicket.InsertAndFetchTicketDtoAsync(existingProject.Guid, existingUser.Guid);
            var existingTicketTwo = await TestTicket.InsertAndFetchTicketDtoAsync(existingProject.Guid, existingUser.Guid);

            await _dataAccess.ExecuteAsync(new InsertProjectTicket(existingProject.Id, existingTicketOne.Id));
            await _dataAccess.ExecuteAsync(new InsertProjectTicket(existingProject.Id, existingTicketTwo.Id));

            var results = await _dataAccess.FetchListAsync(new GetProjectTickets(existingProject.Id));

            await _dataAccess.ExecuteAsync(new DeleteProjectTicket(existingProject.Id, existingTicketOne.Id));
            await _dataAccess.ExecuteAsync(new DeleteProjectTicket(existingProject.Id, existingTicketTwo.Id));

            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(existingTicketOne.Guid));
            await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(existingTicketTwo.Guid));
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(existingProject.Guid));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));

            var ticketOneResult = results.Single(_=>_.Id == existingTicketOne.Id);
            var ticketTwoResult = results.Single(_=>_.Id == existingTicketTwo.Id);

            Assert.Equal(existingTicketOne.Id, ticketOneResult.Id);
            Assert.Equal(existingTicketOne.Guid, ticketOneResult.Guid);
            Assert.Equal(existingTicketOne.Title, ticketOneResult.Title);
            Assert.Equal(existingTicketOne.Description, ticketOneResult.Description);
            Assert.Equal(existingTicketOne.PriorityId, ticketOneResult.PriorityId);
            Assert.Equal(existingTicketOne.StatusId, ticketOneResult.StatusId);
            Assert.Equal(existingTicketOne.DateCreated, ticketOneResult.DateCreated);
            Assert.Equal(existingTicketOne.LastModified, ticketOneResult.LastModified);
            Assert.Equal(existingTicketOne.CreatedByUserId, ticketOneResult.CreatedByUserId);

            Assert.Equal(existingTicketTwo.Id, ticketTwoResult.Id);
            Assert.Equal(existingTicketTwo.Guid, ticketTwoResult.Guid);
            Assert.Equal(existingTicketTwo.Title, ticketTwoResult.Title);
            Assert.Equal(existingTicketTwo.Description, ticketTwoResult.Description);
            Assert.Equal(existingTicketTwo.PriorityId, ticketTwoResult.PriorityId);
            Assert.Equal(existingTicketTwo.StatusId, ticketTwoResult.StatusId);
            Assert.Equal(existingTicketTwo.DateCreated, ticketTwoResult.DateCreated);
            Assert.Equal(existingTicketTwo.LastModified, ticketTwoResult.LastModified);
            Assert.Equal(existingTicketTwo.CreatedByUserId, ticketTwoResult.CreatedByUserId);
        }
    }
}
