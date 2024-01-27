using TicketMate.Application.Requests.UserRequests.GetByGuid;

namespace TicketMate.Application.Tests.RequestTests.UserRequestTests.GetUserByGuidTests
{
    public class GetUserByGuidRequestTests
    {
        [Fact]
        public void GetUserByGuidRequest_Given_EventGuidNotSet_IsValid_ShouldReturn_False()
        {
            var request = new GetUserByGuidRequest();

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void GetUserByGuidRequest_Given_EventGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new GetUserByGuidRequest(Guid.Empty);

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void GetUserByGuidRequest_Given_EventGuidIsValidGuid_IsValid_ShouldReturn_True()
        {
            var request = new GetUserByGuidRequest(Guid.NewGuid());

            Assert.True(request.IsValid(out _));
        }
    }
}
