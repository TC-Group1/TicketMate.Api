using TicketMate.Application.Requests.ProjectTicketRequests.Delete;
using TicketMate.Domain.Models;

namespace TicketMate.Application.Tests.RequestTests.ProjectTicketRequestTests.DeleteProjectTicketTests
{
    public class DeleteProjectTicketRequestValidationTests
    {
        [Fact]
        public void DeleteProjectTicketRequest_Given_TicketNotSet_IsValid_ShouldReturn_False()
        {
            var request = new DeleteProjectTicketRequest() {TicketGuid = Guid.NewGuid()};

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteProjectTicketRequest_Given_ProjectNotSet_IsValid_ShouldReturn_False()
        {
            var request = new DeleteProjectTicketRequest() {ProjectGuid = Guid.NewGuid()} ;

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteProjectTicketRequest_Given_TicketAndProjectNotSet_IsValid_ShouldReturn_False()
        {
            var request = new DeleteProjectTicketRequest();

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteProjectTicketRequest_Given_TicketGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new DeleteProjectTicketRequest(Guid.Empty, Guid.NewGuid());

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteProjectTicketRequest_Given_ProjectGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new DeleteProjectTicketRequest(Guid.NewGuid(), Guid.Empty);

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteProjectTicketRequest_Given_TicketAndProjectGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new DeleteProjectTicketRequest(Guid.Empty, Guid.Empty);

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteProjectTicketRequest_Given_TicketGuidAndProjectGuidIsValidGuid_IsValid_ShouldReturn_True()
        {
            var request = new DeleteProjectTicketRequest(Guid.NewGuid(), Guid.NewGuid());

            Assert.True(request.IsValid(out _));
        }
    }
}
