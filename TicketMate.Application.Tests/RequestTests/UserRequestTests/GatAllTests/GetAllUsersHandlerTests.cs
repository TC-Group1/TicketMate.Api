using GenFu;
using TicketMate.Application.Requests.UserRequests.GetAll;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.DataTransferObjects;

namespace TicketMate.Application.Tests.RequestTests.UserRequestTests.GatAllTests
{
    public class GetAllUsersHandlerTests : HandlerTest
    {
        private readonly GetAllUsersHandler _handler;

        public GetAllUsersHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetAllUsers_Given_UserDTOsReturned_ShouldReturn_CollectionOfUsers()
        {
            var dtos = A.ListOf<Users_DTO>();

            var expected = dtos.Select(_ => _.AsDomainUser());

            SetupMockFetchListAsync<GetAllUsers, Users_DTO>(dtos);

            var result = await _handler.GetResponseAsync(new());

            Assert.Equivalent(expected, result);
        }

        [Fact]
        public async Task GetAllUsers_Given_NoUsersReturned_ShouldReturn_EmptyCollectionOfUsers()
        {
            SetupMockFetchListAsync<GetAllUsers, Users_DTO>(Enumerable.Empty<Users_DTO>());
            
            var result = await _handler.GetResponseAsync(new());

            Assert.Empty(result);
        }
    }
}
