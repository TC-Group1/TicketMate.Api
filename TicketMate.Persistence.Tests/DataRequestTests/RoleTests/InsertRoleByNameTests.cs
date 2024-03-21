using TicketMate.Persistence.DataRequestObjects.RolesRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests.RoleTests
{
    public class InsertRoleByNameTests : BaseDataRequestTest
    {
        [Fact]
        public async Task InsertRole_Given_RoleIsInserted_Should_ReturnOneRowAffected()
        {
            var roleName = "yoda";

            var request = new InsertRoleByName(roleName);

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            // Delete inserted record
            await _dataAccess.ExecuteAsync(new DeleteRoleByName(roleName));

            Assert.Equal(1, rowsAffected);
        }
    }
}
