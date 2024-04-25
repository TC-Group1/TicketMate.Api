using TicketMate.Persistence.DataRequestObjects.RolesRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests.RoleTests
{
    public class DeleteRoleByNameTests : BaseDataRequestTest
    {
        [Fact]
        public async Task DeleteRoleByName_Given_RoleIsDeleted_ShouldReturn_ZeroRowsAffected()
        {
            Assert.Equal(0, await _dataAccess.ExecuteAsync(new DeleteRoleByName(TestString.Random(15))));
        }

        [Fact]
        public async Task DeleteRoleByName_Given_RoleNotDeleted_ShouldReturn_OneRowAffected()
        {
            var roleName = TestString.Random(15);

            await _dataAccess.ExecuteAsync(new InsertRoleByName(roleName));

            var roleBeforeDeleting = await _dataAccess.FetchAsync(new GetRoleByName(roleName));

            var rowsAffectedWhenDeleting = await _dataAccess.ExecuteAsync(new DeleteRoleByName(roleName));

            var roleAfterDeleting = await _dataAccess.FetchAsync(new GetRoleByName(roleName));

            Assert.NotNull(roleBeforeDeleting);

            Assert.Equal(1, rowsAffectedWhenDeleting);

            Assert.Null(roleAfterDeleting);
        }
    }
}
