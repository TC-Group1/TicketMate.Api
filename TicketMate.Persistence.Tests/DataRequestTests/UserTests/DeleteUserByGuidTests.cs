using TicketMate.Persistence.DataRequestObjects.UserRequests;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserTests
{
    public class DeleteUserByGuidTests : BaseDataRequestTest
    {
        [Fact]
        public async Task DeleteUserByGuid_Given_UserIsDeleted_ShouldReturn_ZeroRowsAffected()
        {
            Assert.Equal(0, await _dataAccess.ExecuteAsync(new DeleteUserByGuid(Guid.NewGuid())));
        }

        [Fact]
        public async Task DeleteUserByGuid_Given_UserNotDeleted_ShouldReturn_OneRowAffected()
        {
            var guid = Guid.NewGuid();

            // insert user to delete
            await _dataAccess.ExecuteAsync(new InsertUser(
                                        guid, $"FirstName-{guid}",
                                        $"LastName-{guid}",
                                        $"PhoneNumber-{guid}",
                                        $"Email-{guid}",
                                        $"Avatar-{guid}",
                                        1,
                                        $"PWHash-{guid}"));

            // get user by guid to ensure it was inserted
            var userBeforeDeleting = await _dataAccess.FetchAsync(new GetUserByGuid(guid));

            // delete user and get rows affected
            var rowsAffectedWhenDeleting = await _dataAccess.ExecuteAsync(new DeleteUserByGuid(guid));

            // get user by guid after deleting to ensure it is now null
            var userAfterDeleting = await _dataAccess.FetchAsync(new GetUserByGuid(guid));

            Assert.NotNull(userBeforeDeleting);

            Assert.Equal(1, rowsAffectedWhenDeleting);

            Assert.Null(userAfterDeleting);
        }
    }
}
