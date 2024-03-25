using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using TicketMate.Persistence.DataRequestObjects.RolesRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.DataRequestObjects.UserRolesRequests;
using TicketMate.Persistence.DataTransferObjects;
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

        [Fact]
        public async Task InsertUserRole_Given_UserDoesNotExist_ShouldThrow_MySqlException()
        {
            var roleName = TestString.Random(15);

            // Insert Test Role //
            var insertRoleRequest = new InsertRoleByName(roleName);

            await _dataAccess.ExecuteAsync(insertRoleRequest);

            var roleDto = await _dataAccess.FetchAsync(new GetRoleByName(roleName));

            Assert.NotNull(roleDto);

            // Insert Test UserRole //
            var request = new InsertUserRole(RandomId.Random(), roleDto.Id);

            // assert that request throws MySqlException
            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            Assert.IsType<MySqlException>(exception);

            // Delete Inserted records
            await _dataAccess.ExecuteAsync(new DeleteRoleByName(roleName));

        }

        [Fact]
        public async Task InsertUserRole_Given_RoleDoesNotExist_ShouldThrow_MySqlException()
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

            Assert.NotNull(userDto);

            // Insert Test UserRole //
            var request = new InsertUserRole(userDto.Id, RandomId.Random());

            // assert that request throws MySqlException
            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            Assert.IsType<MySqlException>(exception);

            // Delete Inserted records
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(guid));
        }
    }
}
