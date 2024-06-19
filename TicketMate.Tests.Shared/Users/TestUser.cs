using System.Diagnostics.CodeAnalysis;
using TicketMate.Persistence.Abstraction;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.DataTransferObjects;
using TicketMate.Tests.Shared.Helpers;

namespace TicketMate.Tests.Shared.Users
{
    /// <summary>
    /// Shared Test Helper Used to Insert and Fetch a User_DTO
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class TestUser
    {
        private static IDataAccess _dataAccess = TestDataAccess.SharedInstance;

        public static async Task<Users_DTO> InsertAndFetchUsersDtoAsync()
        {
            var guid = Guid.NewGuid();

            var insertUserRequest = new InsertUser(
                                        guid, TestString.Random(),
                                        TestString.Random(),
                                        TestString.Random(15),
                                        TestString.Random(),
                                        TestString.Random(),
                                        true,
                                        TestString.Random());

            await _dataAccess.ExecuteAsync(insertUserRequest);

            var result = await _dataAccess.FetchAsync(new GetUserByGuid(guid));

            return result!;
        }
    }
}
