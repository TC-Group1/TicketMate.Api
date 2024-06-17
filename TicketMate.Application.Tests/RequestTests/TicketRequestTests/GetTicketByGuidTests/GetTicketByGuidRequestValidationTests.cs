using TicketMate.Application.Requests.TicketRequests.GetByGuid;

namespace TicketMate.Application.Tests.RequestTests.TicketRequestTests.GetTicketByGuidTests
{
    public class GetTicketByGuidRequestValidationTests
    {
        [Fact]
        public void GetTicketByGuidRequest_Given_TicketGuidNotSet_IsValid_ShouldReturn_False()
        {
            var request = new GetTicketByGuidRequest();

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void GetTicketByGuidRequest_Given_TicketGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new GetTicketByGuidRequest(Guid.Empty);

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void GetTicketByGuidRequest_Given_TicketGuidIsValidGuid_IsValid_ShouldReturn_True()
        {
            var request = new GetTicketByGuidRequest(Guid.NewGuid());

            Assert.True(request.IsValid(out _));
        }
    }
}
