using TicketMate.Persistence.DataRequestObjects.RolesRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.DataRequestObjects.UserRolesRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserRoleTests
{
    public class InsertUserRoleTests : BaseDataRequestTest
    {
        [Fact]
        public async Task InsertUserRole_Given_UserRoleIsInserted_Should_ReturnOneRowAffected()
        {
            var guid = Guid.NewGuid();
            var roleName = TestString.Random(15);

            // Insert Test User //
            var insertUserRequest = new InsertUser(
                                        guid, TestString.Random(),
                                        TestString.Random(),
                                        TestString.Random(15),
                                        TestString.Random(),
                                        TestString.Random(),
                                        1,
                                        TestString.Random());

            await _dataAccess.ExecuteAsync(insertUserRequest);

            var userDto = await _dataAccess.FetchAsync(new GetUserByGuid(guid));

            // Insert Test Role //
            var insertRoleRequest = new InsertRoleByName(roleName);

            await _dataAccess.ExecuteAsync(insertRoleRequest);

            var roleDto = await _dataAccess.FetchAsync(new GetRoleByName(roleName));

            Assert.NotNull(userDto);
            Assert.NotNull(roleDto);

            // Insert Test UserRole //
            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertUserRole(userDto.Id, roleDto.Id));

            // Delete Inserted records
            await _dataAccess.ExecuteAsync(new DeleteUserRolesByUserId(userDto.Id));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(guid));
            await _dataAccess.ExecuteAsync(new DeleteRoleByName(roleName));

            Assert.Equal(1, rowsAffected);
        }
    }
}
