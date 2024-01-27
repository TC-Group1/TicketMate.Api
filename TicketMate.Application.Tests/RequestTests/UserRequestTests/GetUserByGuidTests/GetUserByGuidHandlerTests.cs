using GenFu;
using TicketMate.Application.Requests.UserRequests.GetByGuid;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.DataTransferObjects;

namespace TicketMate.Application.Tests.RequestTests.UserRequestTests.GetUserByGuidTests
{
    public class GetUserByGuidHandlerTests : HandlerTest
    {
        private readonly GetUserByGuidRequest _request = new();

        private readonly GetUserByGuidHandler _handler;

        public GetUserByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetUserByGuid_Given_UserNotFoundWithGuid_ShouldThrow_DoesNotExistException()
        {
            SetupMockFetchAsync<GetUserByGuid, Users_DTO>(null);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task GetUserByGuid_Given_UserFoundWithGuid_ShouldReturn_ExpectedUser()
        {
            var userDTO = A.New<Users_DTO>();

            var expected = userDTO.AsDomainUser();

            SetupMockFetchAsync<GetUserByGuid, Users_DTO>(userDTO);

            var response = await _handler.HandleAsync(_request) as GetUserByGuidResponse;

            Assert.NotNull(response);
            Assert.Equivalent(expected, response.User);
        }
    }
}
