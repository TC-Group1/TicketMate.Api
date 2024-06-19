using TicketMate.Application.Requests.UserRequests.UpdateByGuid;
using TicketMate.Domain.Constants;
using TicketMate.Tests.Shared.Helpers;

namespace TicketMate.Application.Tests.RequestTests.UserRequestTests.UpdateUserByGuidTests
{
    public class UpdateUserByGuidRequestTests
    {
        [Fact]
        public void UpdateUserByGuidRequest_Given_ValidData_IsValid_ShouldReturn_True()
        {
            var guid = Guid.NewGuid();

            
            var request = new UpdateUserByGuidRequest(guid,
                                        TestString.Random(),
                                        TestString.Random(),
                                        TestString.Random(14),
                                        TestString.Random(),
                                        TestString.Random(),
                                        true,
                                        TestString.Random());
            var isValid = request.IsValid(out _);

            Assert.True(isValid, "Request should be considered valid.");
            
        }

        [Fact]
        public void UpdateUserByGuidRequest_EmptyGuid_IsValid_ShouldReturnFalse()
        {
            var request = new UpdateUserByGuidRequest(
                Guid.Empty,
                TestString.Random(),
                TestString.Random(),
                TestString.Random(14),
                TestString.Random(),
                TestString.Random(),
                true,
                TestString.Random());

            var isValid = request.IsValid(out _);

            Assert.False(isValid, "Request should be considered invalid.");
        }

        public static IEnumerable<object[]> UpdateRequestExceedingMaxLength = new[]
        {
            new object[]
            {
                new UpdateUserByGuidRequest(//FirstName is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName + 1),
                    TestString.Random(MaxLength.LastName),
                    TestString.Random(MaxLength.PhoneNumber),
                    TestString.Random(MaxLength.Email),
                    TestString.Random(MaxLength.Avatar),
                    true,
                    TestString.Random(MaxLength.PasswordHash))
            },
            new object[]
            {
                new UpdateUserByGuidRequest(//LastName is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName),
                    TestString.Random(MaxLength.LastName + 1),
                    TestString.Random(MaxLength.PhoneNumber),
                    TestString.Random(MaxLength.Email),
                    TestString.Random(MaxLength.Avatar),
                    true,
                    TestString.Random(MaxLength.PasswordHash))
            },
            new object[]
            {
                new UpdateUserByGuidRequest(//PhoneNumber is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName),
                    TestString.Random(MaxLength.LastName),
                    TestString.Random(MaxLength.PhoneNumber + 1),
                    TestString.Random(MaxLength.Email),
                    TestString.Random(MaxLength.Avatar),
                    true,
                    TestString.Random(MaxLength.PasswordHash))
            },
            new object[]
            {
                new UpdateUserByGuidRequest(//Email is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName),
                    TestString.Random(MaxLength.LastName),
                    TestString.Random(MaxLength.PhoneNumber),
                    TestString.Random(MaxLength.Email + 1),
                    TestString.Random(MaxLength.Avatar),
                    true,
                    TestString.Random(MaxLength.PasswordHash))
            },
              new object[]
            {
                new UpdateUserByGuidRequest(//Avatar is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName),
                    TestString.Random(MaxLength.LastName),
                    TestString.Random(MaxLength.PhoneNumber),
                    TestString.Random(MaxLength.Email),
                    TestString.Random(MaxLength.Avatar + 1),
                    true,
                    TestString.Random(MaxLength.PasswordHash))
            },
                new object[]
            {
                new UpdateUserByGuidRequest(//PasswordHash is too long
                    Guid.NewGuid(),
                    TestString.Random(MaxLength.FirstName),
                    TestString.Random(MaxLength.LastName),
                    TestString.Random(MaxLength.PhoneNumber),
                    TestString.Random(MaxLength.Email),
                    TestString.Random(MaxLength.Avatar),
                    true,
                    TestString.Random(MaxLength.PasswordHash + 1))
            }
        };

        [Theory]
        [MemberData(nameof(UpdateRequestExceedingMaxLength))]
        public void UpdateUserByGuidRequest_IfFieldExceedingMaxLength_IsValid_ShouldReturnFalse(UpdateUserByGuidRequest invalidUpdateRequest)
        {
            var isValid = invalidUpdateRequest.IsValid(out _);

            Assert.False(isValid, "Request should be considered invalid due to field exceeding max length.");
        }

    }
}
