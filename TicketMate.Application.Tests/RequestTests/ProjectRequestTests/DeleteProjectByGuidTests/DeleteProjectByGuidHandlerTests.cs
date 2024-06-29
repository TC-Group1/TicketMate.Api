using GenFu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Application.Requests.ProjectRequests.DeleteByGuid;
using TicketMate.Application.Requests.ProjectRequests.GetByGuid;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataTransferObjects;

namespace TicketMate.Application.Tests.RequestTests.ProjectRequestTests.DeleteProjectByGuidTests
{
    public class DeleteProjectByGuidHandlerTests : HandlerTest
    {
        private readonly DeleteProjectByGuidRequest _request = new();

        private readonly DeleteProjectByGuidHandler _handler;

        public DeleteProjectByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteProjectByGuid_Given_ProjectExists_ShouldReturn_TaskCompletedSuccessfully()
        {
            SetupMockFetchAsync<GetProjectByGuid, Projects_DTO>(A.New<Projects_DTO>());

            SetupMockExecuteAsync<DeleteProjectByGuid>(1);

            var task = _handler.HandleAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task DeleteProjectByGuid_Given_ProjectDoesNotExist_ShouldReturn_NotFoundException()
        {
            SetupMockFetchAsync<GetProjectByGuid, Projects_DTO>(null);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }
    }
}
