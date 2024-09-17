using TicketMate.Application.Requests.ProjectRequests.UpdateByGuid;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Application.Tests.RequestTests.ProjectRequestTests.UpdateProjectByGuidTests
{
    public class UpdateProjectByGuidRequestTests
    {
        #region Valid Data
        [Fact]
        public void UpdateProjectByGuidRequest_Given_ValidData_IsValid_ShouldReturn_True()
        {
            var guid = Guid.NewGuid();

            var request = new UpdateProjectByGuidRequest()
            {
                Guid = guid,
                Name = TestString.Random(25)
            };

            var isValid = request.IsValid(out _);

            Assert.True(isValid, "Request should be considered valid.");
        }
        #endregion

        #region Some Data Missing
        [Fact]
        public void UpdateProjectByGuidRequest_Given_NoName_IsValid_ShouldReturn_True()
        {
            var guid = Guid.NewGuid();
            var isActive = false;

            var request = new UpdateProjectByGuidRequest()
            {
                Guid = guid,
                IsActive = isActive
            };

            var isValid = request.IsValid(out _);

            Assert.True(isValid, "Request should be considered valid.");
        }

        [Fact]
        public void UpdateProjectByGuidRequest_Given_NoIsActive_IsValid_ShouldReturn_True()
        {
            var guid = Guid.NewGuid();

            var request = new UpdateProjectByGuidRequest()
            {
                Guid = guid,
                Name = TestString.Random(25)
            };

            var isValid = request.IsValid(out _);

            Assert.True(isValid, "Request should be considered valid.");
        }

        [Fact]
        public void UpdateProjectByGuidRequest_EmptyGuid_IsValid_ShouldReturnFalse()
        {
            var request = new UpdateProjectByGuidRequest()
            {
                Name = TestString.Random(25)
            };

            var isValid = request.IsValid(out _);

            Assert.False(isValid, "Request should be considered invalid.");
        }
        #endregion

        [Fact]
        public void UpdateProjectRequest_Given_NameIsTooLong_IsValid_ShouldReturnFalse()
        {
            var request = new UpdateProjectByGuidRequest()
            {
                Guid = Guid.NewGuid(),
                Name = TestString.Random(150),
            };

            Assert.False(request.IsValid(out _));
        }
    }
}
