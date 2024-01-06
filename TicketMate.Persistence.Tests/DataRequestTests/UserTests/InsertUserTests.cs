using MySql.Data.MySqlClient;
using TicketMate.Persistence.DataRequestObjects.UserRequests;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserTests
{
    public class InsertUserTests : BaseDataRequestTest
    {
        [Fact]
        public async Task InsertUser_Given_UserIsInserted_Should_ReturnOneRowAffected()
        {
            var guid = Guid.NewGuid();

            var request = new InsertUser(guid, $"Username-{guid}", "PwHash");

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            // Delete inserted record
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(request.Guid));

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task InsertUser_Given_GuidAlreadyTaken_ShouldThrow_SqlException()
        {
            var guid = Guid.NewGuid();

            // insert user with Guid so it is already taken
            await _dataAccess.ExecuteAsync(new InsertUser(guid, $"Username-{guid}", "PwHash"));

            // Create request with guid that was just inserted
            var request = new InsertUser(guid, $"Username-{Guid.NewGuid()}", "PwHash");

            // assert that request throws MySqlException
            await Assert.ThrowsAsync<MySqlException>(async () => await _dataAccess.ExecuteAsync(request));

            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(request.Guid));
        }

        [Fact]
        public async Task InsertUser_Given_UsernameAlreadyTaken_ShouldThrow_SqlException()
        {
            var username = Guid.NewGuid().ToString();

            var requestOne = new InsertUser(Guid.NewGuid(), username, "pwHash");
            var requestWithSameUsername = new InsertUser(Guid.NewGuid(), username, "pwHash");

            // insert requestOne so that username is already taken
            await _dataAccess.ExecuteAsync(requestOne);

            // assert that requestWithSameUsername throws MySqlException
            await Assert.ThrowsAsync<MySqlException>(async () => await _dataAccess.ExecuteAsync(requestWithSameUsername));

            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(requestOne.Guid));
        }
    }
}
