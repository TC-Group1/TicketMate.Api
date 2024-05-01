using TicketMate.Application.Requests.ProjectRequests.GetByGuid;

namespace TicketMate.Application.Tests.RequestTests.ProjectRequestTests.GetProjectByGuidTests
{
    public class GetProjectByGuidRequestTests
    {
        [Fact]
        public void GetProjectByGuidRequest_Given_EventGuidNotSet_IsValid_ShouldReturn_False()
        {
            var request = new GetProjectByGuidRequest();

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void GetProjectByGuidRequest_Given_EventGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new GetProjectByGuidRequest(Guid.Empty);

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void GetProjectByGuidRequest_Given_EventGuidIsValidGuid_IsValid_ShouldReturn_True()
        {
            var request = new GetProjectByGuidRequest(Guid.NewGuid());

            Assert.True(request.IsValid(out _));
        }
    }
}
