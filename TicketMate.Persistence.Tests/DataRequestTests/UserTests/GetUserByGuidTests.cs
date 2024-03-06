using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

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

            var insertUserRequest = new InsertUser(
                                        guid, TestString.Random(),
                                        TestString.Random(),
                                        TestString.Random(15),
                                        TestString.Random(),
                                        TestString.Random(),
                                        1,
                                        TestString.Random());

            await _dataAccess.ExecuteAsync(insertUserRequest);
        
            var result = await _dataAccess.FetchAsync(new GetUserByGuid(guid));

            // Delete inserted user
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(guid));

            Assert.NotNull(result);
            Assert.Equal(insertUserRequest.Guid, result.Guid);
            Assert.Equal(insertUserRequest.PhoneNumber, result.PhoneNumber);
            Assert.Equal(insertUserRequest.FirstName, result.FirstName);
            Assert.Equal(insertUserRequest.LastName, result.LastName);
            Assert.Equal(insertUserRequest.Email, result.Email);
            Assert.Equal(insertUserRequest.Avatar, result.Avatar);
            Assert.Equal(insertUserRequest.IsActive, result.IsActive);
            Assert.Equal(insertUserRequest.PasswordHash, result.PasswordHash);
        }
    }
}
