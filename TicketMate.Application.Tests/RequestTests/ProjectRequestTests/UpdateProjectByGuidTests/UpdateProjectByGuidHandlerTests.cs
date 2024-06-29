using Moq;
using TicketMate.Application.Requests.ProjectRequests.UpdateByGuid;
using TicketMate.Domain.Constants;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Tests.Shared.Helpers;

namespace TicketMate.Application.Tests.RequestTests.ProjectRequestTests.UpdateProjectByGuidTests
{
    public class UpdateProjectByGuidHandlerTests : HandlerTest
    {
        private readonly UpdateProjectByGuidRequest _request = new();

        private readonly UpdateProjectByGuidHandler _handler;

        public UpdateProjectByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task UpdateProjectHandler_Given_ProjectIsUpdated_ShouldReturn_TaskCompletedSuccessfully()
        {
            var rowsAffected = 1;

            SetupMockExecuteAsync<UpdateProjectByGuid>(rowsAffected);

            var task = _handler.ExecuteRequestAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task UpdateProjectHandler_Given_UpdateProjectReturnsNoRowsAffected_ShouldReturn_OperationFailedException()
        {
            var rowsAffected = 0;

            SetupMockExecuteAsync<UpdateProjectByGuid>(rowsAffected);

            var exception = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(_request));

            Assert.IsType<OperationFailedException>(exception);
        }

        [Fact]
        public async Task UpdateProjectByGuid_Given_DuplicateEntry_ShouldThrow_AlreadyExistsException()
        {
            var guid = Guid.NewGuid();
            var request = new UpdateProjectByGuidRequest
            {
                Guid = guid,
                Name = TestString.Random(25)
            };

            var ex = new DataAccessException("Duplicate entry 'Project for key 'Projects", MySqlExceptionNumber.DuplicateEntry, null!);

            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<UpdateProjectByGuid>())).Throws(ex);

            var result = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(request));

            Assert.IsType<AlreadyExistsException>(result);
        }
    }
}
