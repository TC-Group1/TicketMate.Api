using MySql.Data.MySqlClient;
using TicketMate.Persistence.DataRequestObjects.RolesRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests.RoleTests
{
    public class InsertRoleByNameTests : BaseDataRequestTest
    {
        [Fact]
        public async Task InsertRole_Given_RoleIsInserted_Should_ReturnOneRowAffected()
        {
            var roleName = TestString.Random(15);

            var request = new InsertRoleByName(roleName);

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            await _dataAccess.ExecuteAsync(new DeleteRoleByName(roleName));

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task InsertRole_Given_RoleNameAlreadyTaken_ShouldThrow_MySqlException()
        {
            var roleName = TestString.Random(15);

            await _dataAccess.ExecuteAsync(new InsertRoleByName(roleName));

            var request = new InsertRoleByName(roleName);

            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            Assert.IsType<MySqlException>(exception);

            await _dataAccess.ExecuteAsync(new DeleteRoleByName(roleName));
        }
    }
}
