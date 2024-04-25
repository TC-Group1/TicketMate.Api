using GenFu;
using TicketMate.Application.Requests.ProjectRequests.GetByGuid;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataTransferObjects;

namespace TicketMate.Application.Tests.RequestTests.ProjectRequestTests.GetProjectByGuidTests
{
    public class GetProjectByGuidHandlerTests : HandlerTest
    {
        private readonly GetProjectByGuidRequest _request = new();

        private readonly GetProjectByGuidHandler _handler;

        public GetProjectByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetProjectByGuid_Given_ProjectNotFoundWithGuid_ShouldThrow_DoesNotExistException()
        {
            SetupMockFetchAsync<GetProjectByGuid, Projects_DTO>(null);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task GetProjectByGuid_Given_ProjectFoundWithGuid_ShouldReturn_ExpectedProject()
        {
            var projectsDTO = A.New<Projects_DTO>();

            var expected = projectsDTO.AsDomainProject();

            SetupMockFetchAsync<GetProjectByGuid, Projects_DTO>(projectsDTO);

            var response = await _handler.HandleAsync(_request) as GetProjectByGuidResponse;

            Assert.NotNull(response);
            Assert.Equivalent(expected, response.Project);
        }
    }
}
