using TicketMate.Application.Requests.UserRequests.DeleteByGuid;

namespace TicketMate.Application.Tests.RequestTests.UserRequestTests.DeleteUserByGuidTests
{
    public class DeleteUserByGuidRequestTests
    {
        [Fact]
        public void DeleteUserByGuidRequest_Given_EventGuidNotSet_IsValid_ShouldReturn_False()
        {
            var request = new DeleteUserByGuidRequest();

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteUserByGuidRequest_Given_EventGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new DeleteUserByGuidRequest(Guid.Empty);

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteUserByGuidRequest_Given_EventGuidIsValidGuid_IsValid_ShouldReturn_True()
        {
            var request = new DeleteUserByGuidRequest(Guid.NewGuid());

            Assert.True(request.IsValid(out _));
        }
    }
}
