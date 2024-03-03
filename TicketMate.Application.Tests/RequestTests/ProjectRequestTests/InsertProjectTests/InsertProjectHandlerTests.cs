using Moq;
using TicketMate.Application.Requests.ProjectRequests.Insert;
using TicketMate.Application.Tests.Helpers;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;

namespace TicketMate.Application.Tests.RequestTests.ProjectRequestTests.InsertProjectTests
{
    public class InsertProjectHandlerTests : HandlerTest
    {
        private readonly InsertProjectRequest _request = new();

        private readonly InsertProjectHandler _handler;

        public InsertProjectHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task InsertProjectHandler_Given_ProjectIsInserted_ShouldReturn_TaskCompletedSuccessfully()
        {
            var rowsAffected = 1;

            SetupMockExecuteAsync<InsertProject>(rowsAffected);

            var task = _handler.ExecuteRequestAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task InsertProjectHandler_Given_InsertProjectReturnsNoRowsAffected_ShouldReturn_OperationFailedException()
        {
            var rowsAffected = 0;

            SetupMockExecuteAsync<InsertProject>(rowsAffected);

            var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<OperationFailedException>(exception);
        }

        [Fact]
        public async Task InsertProjectHandler_Given_MySqlException_DuplicateGuid_ShouldThrow_AlreadyExistsException()
        {
            var mySqlException = MySqlExceptionHelper.Instantiate("Duplicate entry 'fdfd5be4-ae90-49df-bdd1-37ef67ce4ec8' for key 'Projects.Guid_UNIQUE'");

            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<InsertProject>())).Throws(mySqlException);

            var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<AlreadyExistsException>(exception);
        }

        [Fact]
        public async Task InsertProjectHandler_Given_MySqlException_UnableToConnect_ShouldThrow_OperationFailedException()
        {
            var mySqlException = MySqlExceptionHelper.Instantiate("Unable to connect to any of the specified MySQL hosts.");

            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<InsertProject>())).Throws(mySqlException);

            var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<OperationFailedException>(exception);
        }


    }
}