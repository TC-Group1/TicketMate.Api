﻿using TicketMate.Persistence.DataRequestObjects.RolesRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.DataRequestObjects.UserRolesRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserRoleTests
{
    public class DeleteUserRolesByUserIdTests : BaseDataRequestTest
    {
        [Fact]
        public async Task DeleteUserRolesByUserId_Given_UserRoleNotExisting_ShouldReturn_ZeroRowsAffected()
        {
            Assert.Equal(0, await _dataAccess.ExecuteAsync(new DeleteUserRolesByUserId(RandomId.Random())));
        }

        [Fact]
        public async Task DeleteUserRolesByUserId_Given_UserRolesExists_ShouldReturn_OneRowAffected()
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
            await _dataAccess.ExecuteAsync(new InsertUserRole(userDto.Id, roleDto.Id));

            var result = await _dataAccess.ExecuteAsync(new DeleteUserRolesByUserId(userDto.Id));

            // Delete Inserted records
            await _dataAccess.ExecuteAsync(new DeleteUserRolesByUserId(userDto.Id));
            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(guid));
            await _dataAccess.ExecuteAsync(new DeleteRoleByName(roleName));

            Assert.Equal(1, result);
        }
    }
}