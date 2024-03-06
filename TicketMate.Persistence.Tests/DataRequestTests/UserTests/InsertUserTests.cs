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

            var request = new InsertUser(
                                        guid, $"FirstName-{guid}",
                                        $"LastName-{guid}",
                                        $"PhoneNumber-{guid}",
                                        $"Email-{guid}",
                                        $"Avatar-{guid}",
                                        1,
                                        $"PWHash-{guid}");

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            // Delete inserted record
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(request.Guid));

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task InsertUser_Given_GuidAlreadyTaken_ShouldThrow_MySqlException()
        {
            var guid = Guid.NewGuid();

            // insert user with Guid so it is already taken
            await _dataAccess.ExecuteAsync(new InsertUser(
                                        guid, $"FirstName-{guid}",
                                        $"LastName-{guid}",
                                        $"PhoneNumber-{guid}",
                                        $"Email-{guid}",
                                        $"Avatar-{guid}",
                                        1,
                                        $"PWHash-{guid}"));

            // Create request with guid that was just inserted
            var request = new InsertUser(
                                        guid, $"FirstName-{guid}",
                                        $"LastName-{guid}",
                                        $"PhoneNumber-{guid}",
                                        $"Email-{guid}",
                                        $"Avatar-{guid}",
                                        1,
                                        $"PWHash-{guid}");

            // assert that request throws MySqlException
            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            Assert.IsType<MySqlException>(exception);

            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(request.Guid));
        }

        [Fact]
        public async Task InsertUser_Given_UsernameAlreadyTaken_ShouldThrow_MySqlException()
        {
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString();
            var avatar = Guid.NewGuid().ToString();
            var phoneNumber = Guid.NewGuid().ToString();
            

            var requestOne = new InsertUser(Guid.NewGuid(), firstName, lastName, phoneNumber, email, avatar, 1, "pwHash");
            var requestWithSameUsername = new InsertUser(Guid.NewGuid(), firstName, lastName, phoneNumber, email, avatar, 1, "pwHash");

            // insert requestOne so that username is already taken
            await _dataAccess.ExecuteAsync(requestOne);

            // assert that requestWithSameUsername throws MySqlException
            await Assert.ThrowsAsync<MySqlException>(async () => await _dataAccess.ExecuteAsync(requestWithSameUsername));

            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(requestOne.Guid));
        }
    }
}
