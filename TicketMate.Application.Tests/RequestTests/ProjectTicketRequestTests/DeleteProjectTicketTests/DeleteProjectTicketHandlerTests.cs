using TicketMate.Application.Requests.ProjectTicketRequests.Delete;
using TicketMate.Domain.Exceptions;
using TicketMate.Domain.Models;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataTransferObjects;

namespace TicketMate.Application.Tests.RequestTests.ProjectTicketRequestTests.DeleteProjectTicketTests
{
    public class DeleteProjectTicketHandlerTests : HandlerTest
    {
        private readonly DeleteProjectTicketRequest _request = new();

        private readonly DeleteProjectTicketHandler _handler;
        public DeleteProjectTicketHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteProjectTicket_Given_ProjectTicketIsDeleted_ShouldReturn_TaskCompletedSuccesfully()
        {
            var rowsAffected = 1; 

            SetupMockExecuteAsync<DeleteProjectTicket>(rowsAffected);

            var task = _handler.ExecuteRequestAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task DeleteProjectTicket_Given_TicketDoesNotExist_ShouldThrow_DoesNotExistException()
        {
            var expectedNameOfObjectNotExisting = nameof(Ticket);

            var rowsAffected = 0;

            SetupMockExecuteAsync<DeleteProjectTicket>(rowsAffected);

            SetupMockFetchAsync<GetTicketByGuid, Tickets_DTO>(null);

            var exception = await Record.ExceptionAsync(async ()=> await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<DoesNotExistException>(exception);

            Assert.Equal(expectedNameOfObjectNotExisting, (exception as DoesNotExistException)!.NameOfObjectNotExisting);
        }

        [Fact]
        public async Task DeleteProjectTicket_Given_ProjectDoesNotExist_ShouldThrow_DoesNotExistException()
        {
            var expectedNameOfObjectNotExisting = nameof(Project);

            var rowsAffected = 0;

            SetupMockExecuteAsync<DeleteProjectTicket>(rowsAffected);

            SetupMockFetchAsync<GetTicketByGuid, Tickets_DTO>(new Tickets_DTO());

            SetupMockFetchAsync<GetProjectByGuid, Projects_DTO>(null);

            var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<DoesNotExistException>(exception);

            Assert.Equal(expectedNameOfObjectNotExisting, (exception as DoesNotExistException)!.NameOfObjectNotExisting);
        }

        [Fact]
        public async Task DeleteProjectTicket_Given_ProjectTicketNotDeletedAndProjectAndTicketExist_ShouldThrow_OperationFailedException()
        {
            var rowsAffected = 0;

            SetupMockExecuteAsync<DeleteProjectTicket>(rowsAffected);

            SetupMockFetchAsync<GetTicketByGuid, Tickets_DTO>(new Tickets_DTO());

            SetupMockFetchAsync<GetProjectByGuid, Projects_DTO>(new Projects_DTO());

            var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<OperationFailedException>(exception);
        }
    }
}
