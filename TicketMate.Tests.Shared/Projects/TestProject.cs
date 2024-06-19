using System.Diagnostics.CodeAnalysis;
using TicketMate.Persistence.Abstraction;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataTransferObjects;
using TicketMate.Tests.Shared.Helpers;

namespace TicketMate.Tests.Shared.Projects
{
    /// <summary>
    /// Shared Test Helper Used to Insert and Fetch a Project_DTO
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class TestProject
    {
        private static IDataAccess _dataAccess = TestDataAccess.SharedInstance;

        public static async Task<Projects_DTO> InsertAndFetchProjectDtoAsync()
        {

            var guid = Guid.NewGuid();

            var insertProjectRequest = new InsertProject(guid, "Name");

            await _dataAccess.ExecuteAsync(insertProjectRequest);

            var result = await _dataAccess.FetchAsync(new GetProjectByGuid(guid));

            return result!;
        }
    }
}
