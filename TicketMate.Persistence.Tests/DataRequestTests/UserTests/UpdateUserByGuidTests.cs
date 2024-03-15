using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Domain.Constants;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserTests
{
    public class UpdateUserByGuidTests : BaseDataRequestTest
    {
        [Fact]
        public async Task UpdateUserByGuid_IfUpdateSuccessful_ShouldReturnOneRowAffected()
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

            var updateRequest = new UpdateUserByGuid(
                                        guid,
                                        TestString.Random(),
                                        TestString.Random(),
                                        TestString.Random(14),
                                        TestString.Random(),
                                        TestString.Random(),
                                        1,
                                        TestString.Random());

            var rowsAffected = await _dataAccess.ExecuteAsync(updateRequest);

            Assert.Equal(1, rowsAffected);

            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(guid));
        }


        public static IEnumerable<object[]> UpdateRequestExceedingMaxLength = new[]
        {
            new object[]
            {
                new UpdateUserByGuid(//FirstName is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName + 1),
                    TestString.Random(MaxLength.LastName),
                    TestString.Random(MaxLength.PhoneNumber),
                    TestString.Random(MaxLength.Email),
                    TestString.Random(MaxLength.Avatar),
                    1,
                    TestString.Random(MaxLength.PasswordHash))
            },
            new object[]
            {
                new UpdateUserByGuid(//LastName is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName),
                    TestString.Random(MaxLength.LastName + 1),
                    TestString.Random(MaxLength.PhoneNumber),
                    TestString.Random(MaxLength.Email),
                    TestString.Random(MaxLength.Avatar),
                    1,
                    TestString.Random(MaxLength.PasswordHash))
            },
            new object[]
            {
                new UpdateUserByGuid(//PhoneNumber is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName),
                    TestString.Random(MaxLength.LastName),
                    TestString.Random(MaxLength.PhoneNumber + 1),
                    TestString.Random(MaxLength.Email),
                    TestString.Random(MaxLength.Avatar),
                    1,
                    TestString.Random(MaxLength.PasswordHash))
            },
            new object[]
            {
                new UpdateUserByGuid(//Email is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName),
                    TestString.Random(MaxLength.LastName),
                    TestString.Random(MaxLength.PhoneNumber),
                    TestString.Random(MaxLength.Email + 1),
                    TestString.Random(MaxLength.Avatar),
                    1,
                    TestString.Random(MaxLength.PasswordHash))
            },
              new object[]
            {
                new UpdateUserByGuid(//Avatar is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName),
                    TestString.Random(MaxLength.LastName),
                    TestString.Random(MaxLength.PhoneNumber),
                    TestString.Random(MaxLength.Email),
                    TestString.Random(MaxLength.Avatar + 1),
                    1,
                    TestString.Random(MaxLength.PasswordHash))
            },
                new object[]
            {
                new UpdateUserByGuid(//PasswordHash is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName),
                    TestString.Random(MaxLength.LastName),
                    TestString.Random(MaxLength.PhoneNumber),
                    TestString.Random(MaxLength.Email),
                    TestString.Random(MaxLength.Avatar),
                    1,
                    TestString.Random(MaxLength.PasswordHash + 1))
            }
        };

        [Theory]
        [MemberData(nameof(UpdateRequestExceedingMaxLength))]
        public async Task UpdateUserByGuid_IfFieldExceedingMaxLength_ShouldThrow_MySqlException(UpdateUserByGuid invalidUpdateRequest)
        {
            var createTestUser = new InsertUser(
                                        invalidUpdateRequest.Guid,
                                        TestString.Random(MaxLength.FirstName),
                                        TestString.Random(MaxLength.LastName),
                                        TestString.Random(MaxLength.PhoneNumber),
                                        TestString.Random(MaxLength.Email),
                                        TestString.Random(MaxLength.Avatar),
                                        1,
                                        TestString.Random(MaxLength.PasswordHash));

            await _dataAccess.ExecuteAsync(createTestUser);

            await Assert.ThrowsAsync<MySqlException>(async () => await _dataAccess.ExecuteAsync(invalidUpdateRequest));

            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(invalidUpdateRequest.Guid));
        }

        [Fact]
        public async Task UpdateUserByGuid_IfValuesUpdatedAreAccurate_ShouldPass()
        {
            var guid = Guid.NewGuid();

            var testUser = new InsertUser(
                                        guid,
                                        TestString.Random(MaxLength.FirstName),
                                        TestString.Random(MaxLength.LastName),
                                        TestString.Random(7),
                                        TestString.Random(MaxLength.Email),
                                        TestString.Random(MaxLength.Avatar),
                                        1,
                                        TestString.Random(MaxLength.PasswordHash));

            await _dataAccess.ExecuteAsync(testUser);

            var updateRequest = new UpdateUserByGuid(
                                        guid,
                                        TestString.Random(MaxLength.FirstName),
                                        TestString.Random(MaxLength.LastName),
                                        TestString.Random(14),
                                        TestString.Random(MaxLength.Email),
                                        TestString.Random(MaxLength.Avatar),
                                        1,
                                        TestString.Random(MaxLength.PasswordHash));
            await _dataAccess.ExecuteAsync(updateRequest);

            var getUser = await _dataAccess.FetchAsync(new GetUserByGuid(guid));

            Assert.Equal(updateRequest.FirstName, getUser.FirstName);
            Assert.Equal(updateRequest.LastName, getUser.LastName);
            Assert.Equal(updateRequest.PhoneNumber, getUser.PhoneNumber);
            Assert.Equal(updateRequest.Email, getUser.Email);
            Assert.Equal(updateRequest.Avatar, getUser.Avatar);
            Assert.Equal(updateRequest.IsActive, getUser.IsActive);
            Assert.Equal(updateRequest.PasswordHash, getUser.PasswordHash);

            await _dataAccess.ExecuteAsync(new DeleteUserByGuid(guid));
        }

    }
}
