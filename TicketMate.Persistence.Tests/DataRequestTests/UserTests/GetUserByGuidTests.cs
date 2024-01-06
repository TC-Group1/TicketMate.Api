using TicketMate.Persistence.DataRequestObjects.UserRequests;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserTests
{
    public class GetUserByGuidTests : BaseDataRequestTest
    {
        [Fact]
        public async Task GetUserByGuid_Given_UserNotExisting_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetUserByGuid(Guid.NewGuid())));
        }

        [Fact]
        public async Task GetUserByGuid_Given_UserExists_ShouldReturn_User_DTO()
        {
            var guid = Guid.NewGuid();

            var insertUserRequest = new InsertUser(guid, $"Username-{guid}", $"PWHash-{guid}");

            await _dataAccess.ExecuteAsync(insertUserRequest);
        
            var result = await _dataAccess.FetchAsync(new GetUserByGuid(guid));

            // Delete inserted user
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(guid));

            Assert.NotNull(result);
            Assert.Equal(insertUserRequest.Guid, result.Guid);
            Assert.Equal(insertUserRequest.Username, result.Username);
            Assert.Equal(insertUserRequest.PasswordHash, result.PasswordHash);
        }
    }
}
