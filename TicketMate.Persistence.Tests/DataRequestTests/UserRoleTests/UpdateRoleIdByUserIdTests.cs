using MySql.Data.MySqlClient;
using TicketMate.Persistence.DataRequestObjects.RolesRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.DataRequestObjects.UserRolesRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserRoleTests
{
    public class UpdateRoleIdByUserIdTests : BaseDataRequestTest
    {
        [Fact]
        public async Task UpdateRoleIdByUserId_Given_UserRoleIsUpdated_Should_ReturnOneRowAffected()
        {
            var guid = Guid.NewGuid();
            var adminRole = TestString.Random(15);
            var testerRole = TestString.Random(15);

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

            // Insert Test Roles //
            await _dataAccess.ExecuteAsync(new InsertRoleByName(adminRole));
            await _dataAccess.ExecuteAsync(new InsertRoleByName(testerRole));

            var adminRoleDto = await _dataAccess.FetchAsync(new GetRoleByName(adminRole));
            var testerRoleDto = await _dataAccess.FetchAsync(new GetRoleByName(testerRole));

            Assert.NotNull(userDto);
            Assert.NotNull(adminRoleDto);
            Assert.NotNull(testerRoleDto);

            // Insert Test UserRole //
            await _dataAccess.ExecuteAsync(new InsertUserRole(userDto.Id, adminRoleDto.Id));

            // Update UserRoleByUserId //
            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateRoleIdByUserId(userDto.Id, testerRoleDto.Id));

            // Delete Inserted records
            await _dataAccess.ExecuteAsync(new DeleteUserRolesByUserId(userDto.Id));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(guid));
            await _dataAccess.ExecuteAsync(new DeleteRoleByName(adminRole));
            await _dataAccess.ExecuteAsync(new DeleteRoleByName(testerRole));

            Assert.Equal(1, rowsAffected);
        }


        [Fact]
        public async Task UpdateUserRole_Given_NewRoleDoesNotExist_ShouldThrow_MySqlException()
        {
            var guid = Guid.NewGuid();
            var adminRole = TestString.Random(15);

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
            await _dataAccess.ExecuteAsync(new InsertRoleByName(adminRole));

            var adminRoleDto = await _dataAccess.FetchAsync(new GetRoleByName(adminRole));

            Assert.NotNull(userDto);
            Assert.NotNull(adminRoleDto);

            // Insert Test UserRole //
            await _dataAccess.ExecuteAsync(new InsertUserRole(userDto.Id, adminRoleDto.Id));

            // Update UserRoleByUserId //
            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(new UpdateRoleIdByUserId(userDto.Id, RandomId.Random())));

            Assert.IsType<MySqlException>(exception);

            // Delete Inserted records
            await _dataAccess.ExecuteAsync(new DeleteUserRolesByUserId(userDto.Id));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(guid));
            await _dataAccess.ExecuteAsync(new DeleteRoleByName(adminRole));

        }

    }
}
