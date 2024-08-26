using TicketMate.Application.Requests.TicketRequests.UpdateByGuid;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Application.Tests.RequestTests.TicketRequestTests.UpdateTicketTests
{
    public class UpdateTicketByGuidRequestValidationTests
    {
        [Fact]
        public void UpdateTicketByGuidRequest_Given_ValidData_IsValid_ShouldReturn_True()
        {
            var guid = Guid.NewGuid();

            var request = new UpdateTicketByGuidRequest()
            {
                Guid = guid,
                Title = TestString.Random(25)
            };

            var isValid = request.IsValid(out _);

            Assert.True(isValid, "Request should be considered valid.");
        }

        [Fact]
        public void UpdateTicketByGuidRequest_EmptyGuid_IsValid_ShouldReturnFalse()
        {
            var request = new UpdateTicketByGuidRequest()
            {
                Title = TestString.Random(25)
            };

            var isValid = request.IsValid(out _);

            Assert.False(isValid, "Request should be considered invalid.");
        }

        [Fact]
        public void UpdateTicketRequest_Given_TitleIsTooLong_IsValid_ShouldReturnFalse()
        {
            var request = new UpdateTicketByGuidRequest()
            {
                Guid = Guid.NewGuid(),
                Title = TestString.Random(150),
            };

            Assert.False(request.IsValid(out _));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData(null)]
        public void UpdateTicketByGuidRequest_TitleIsNullEmptyOrWhiteSpace_IsValid_ShouldReturnFalse(string? invalidTitle)
        {
            var guid = Guid.NewGuid();

            var request = new UpdateTicketByGuidRequest()
            {
                Guid = guid,
                Title = invalidTitle!
            };

            var isValid = request.IsValid(out _);

            Assert.False(isValid, "Request should be considered invalid.");
        }
    }
}
