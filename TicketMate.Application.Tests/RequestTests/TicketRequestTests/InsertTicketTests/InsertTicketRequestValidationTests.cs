using TicketMate.Application.Requests.TicketRequests.Insert;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Application.Tests.RequestTests.TicketRequestTests.InsertTicketTests
{
    public class InsertTicketRequestValidationTests
    {
        #region InvalidGuidTests
        [Fact]
        public void InsertTicketRequest_Given_TicketGuidNotProvided_IsValid_ShouldReturnFalse()
        {
            var request = new InsertTicketRequest()
            {
                Title = TestString.Random(20),
                ProjectGuid = Guid.NewGuid(),
                CreatedByUserGuid = Guid.NewGuid(),
            };

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void InsertTicketRequest_Given_TicketGuidIsEmpty_IsValid_ShouldReturnFalse()
        {
            var request = new InsertTicketRequest()
            {
                Guid = Guid.Empty,
                Title = TestString.Random(20),
                ProjectGuid = Guid.NewGuid(),
                CreatedByUserGuid = Guid.NewGuid(),
            };

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void InsertTicketRequest_Given_ProjectGuidNotProvided_IsValid_ShouldReturnFalse()
        {
            var request = new InsertTicketRequest()
            {
                Title = TestString.Random(20),
                Guid = Guid.NewGuid(),
                CreatedByUserGuid = Guid.NewGuid(),
            };

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void InsertTicketRequest_Given_ProjectGuidIsEmpty_IsValid_ShouldReturnFalse()
        {
            var request = new InsertTicketRequest()
            {
                Guid = Guid.NewGuid(),
                Title = TestString.Random(20),
                ProjectGuid = Guid.Empty,
                CreatedByUserGuid = Guid.NewGuid(),
            };

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void InsertTicketRequest_Given_CreatedByUserGuidGuidNotProvided_IsValid_ShouldReturnFalse()
        {
            var request = new InsertTicketRequest()
            {
                Title = TestString.Random(20),
                Guid = Guid.NewGuid(),
                ProjectGuid = Guid.NewGuid(),         
            };

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void InsertTicketRequest_Given_CreatedByUserGuidGuidIsEmpty_IsValid_ShouldReturnFalse()
        {
            var request = new InsertTicketRequest()
            {
                Guid = Guid.NewGuid(),
                Title = TestString.Random(20),
                ProjectGuid = Guid.NewGuid(),
                CreatedByUserGuid = Guid.Empty,
            };

            Assert.False(request.IsValid(out _));
        }
        #endregion

        #region InvalidTitleTests
        [Fact]
        public void InsertTicketRequest_Given_TitleIsTooLong_IsValid_ShouldReturnFalse()
        {
            var request = new InsertTicketRequest()
            {
                Guid = Guid.NewGuid(),
                Title = TestString.Random(150),
                ProjectGuid = Guid.NewGuid(),
                CreatedByUserGuid = Guid.NewGuid(),
            };

            Assert.False(request.IsValid(out _));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData(null)]
        public void InsertTicketRequest_Given_TitleIsNullEmptyOrWhitespace_IsValid_ShouldReturnFalse(string? invalidTitle)
        {
            var request = new InsertTicketRequest()
            {
                Guid = Guid.NewGuid(),
                Title = invalidTitle!,
                ProjectGuid = Guid.NewGuid(),
                CreatedByUserGuid = Guid.NewGuid(),
            };

            Assert.False(request.IsValid(out _));
        }
        #endregion

        #region ValidRequestTest
        [Fact]
        public void InsertTicketRequest_Given_ValidRequest_IsValid_ShouldReturnTrue()
        {
            var request = new InsertTicketRequest()
            {
                Guid = Guid.NewGuid(),
                Title = TestString.Random(20),
                ProjectGuid = Guid.NewGuid(),
                CreatedByUserGuid = Guid.NewGuid(),
            };

            Assert.True(request.IsValid(out _));
        }
        #endregion
    }
}
