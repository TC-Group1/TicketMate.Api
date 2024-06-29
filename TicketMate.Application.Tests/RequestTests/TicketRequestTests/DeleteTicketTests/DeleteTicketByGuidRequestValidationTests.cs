using TicketMate.Application.Requests.TicketRequests.DeleteByGuid;

namespace TicketMate.Application.Tests.RequestTests.TicketRequestTests.DeleteTicketTests
{
    public class DeleteTicketByGuidRequestValidationTests
    {
        [Fact]
        public void DeleteTicketByGuidRequest_Given_TicketGuidNotSet_IsValid_ShouldReturn_False()
        {
            var request = new DeleteTicketByGuidRequest();

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteTicketByGuidRequest_Given_TicketGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new DeleteTicketByGuidRequest(Guid.Empty);

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteTicketByGuidRequest_Given_TicketGuidIsValidGuid_IsValid_ShouldReturn_True()
        {
            var request = new DeleteTicketByGuidRequest(Guid.NewGuid());

            Assert.True(request.IsValid(out _));
        }
    }
}
