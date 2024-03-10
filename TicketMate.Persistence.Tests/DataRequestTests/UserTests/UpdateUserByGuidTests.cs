using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserTests
{
    public class UpdateUserByGuidTests : BaseDataRequestTest
    {
        [Fact]
        public async Task UpdateUserByGuid_UserExists_ShouldReturnOneRowAffected() 
        {
            var guid = Guid.NewGuid();

            var createTestUser = new InsertUser(
                                        guid,
                                        TestString.Random(),
                                        TestString.Random(),
                                        TestString.Random(15),
                                        TestString.Random(),
                                        TestString.Random(),
                                        1,
                                        TestString.Random());
            await _dataAccess.ExecuteAsync(createTestUser);

            var newUserGuid = createTestUser.Guid;

            var updateRequest = new UpdateUserByGuid(
                                        newUserGuid, 
                                        TestString.Random(),
                                        TestString.Random(),
                                        TestString.Random(15),
                                        TestString.Random(),
                                        TestString.Random(),
                                        1,
                                        TestString.Random());

            var rowsAffected = await _dataAccess.ExecuteAsync(updateRequest);

            Assert.Equal(1, rowsAffected);

            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(newUserGuid));
        }

        [Fact]
        public async Task UpdateUserByGuid_IfWrongValuesEntered_ShouldThrow_MySqlException()
        {
            var guid = Guid.NewGuid();

            var createTestUser = new InsertUser(
                                        guid,
                                        TestString.Random(),
                                        TestString.Random(),
                                        TestString.Random(15),
                                        TestString.Random(),
                                        TestString.Random(),
                                        1,
                                        TestString.Random());

            await _dataAccess.ExecuteAsync(createTestUser);

            var newUserGuid = createTestUser.Guid;

            var invalidUpdateUserRequest = new UpdateUserByGuid(
                                        newUserGuid,
                                        TestString.Random(),
                                        TestString.Random(),
                                        TestString.Random(15),
                                        TestString.Random(),
                                        TestString.Random(),
                                        1,
                                        TestString.Random());

            var invalidRequest = await _dataAccess.ExecuteAsync(invalidUpdateUserRequest);

            await Assert.ThrowsAsync<MySqlException>(async () => await _dataAccess.ExecuteAsync(invalidUpdateUserRequest));

            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(newUserGuid));
        }

    }
}
