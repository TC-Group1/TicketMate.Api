using TicketMate.Persistence.DataRequestObjects.RolesRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests.RoleTests
{
    public class GetRoleByNameTests : BaseDataRequestTest
    {
        [Fact]
        public async Task GetRoleByName_Given_RoleNotExisting_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetRoleByName(TestString.Random(15))));
        }

        [Fact]
        public async Task GetRoleByName_Given_RoleExists_ShouldReturn_Role_DTO()
        {
            var roleName = TestString.Random(15);

            var insertRoleRequest = new InsertRoleByName(roleName);

            await _dataAccess.ExecuteAsync(insertRoleRequest);

            var result = await _dataAccess.FetchAsync(new GetRoleByName(roleName));

            // Delete inserted role
            await _dataAccess.ExecuteAsync(new DeleteRoleByName(roleName));

            Assert.NotNull(result);
            Assert.Equal(insertRoleRequest.Name, result.Name, ignoreCase: true);
        }
    }
}
