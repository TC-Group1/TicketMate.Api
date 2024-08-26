using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserTests
{
    public class GetAllUsersTests : BaseDataRequestTest
    {
        [Fact]
        public async Task GetAllUsers_Given_UserExists_Should_ReturnExistingUser()
        {
            var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();

            var result = await _dataAccess.FetchListAsync(new GetAllUsers());

            Assert.NotNull(result.Single(_ => _.Id == existingUser.Id));
        }
    }
}
