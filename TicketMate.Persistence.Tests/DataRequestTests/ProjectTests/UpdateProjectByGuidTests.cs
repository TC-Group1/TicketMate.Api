using TicketMate.Domain.Constants;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Persistence.Tests.DataRequestTests.ProjectTests
{
    public class UpdateProjectByGuidTests : BaseDataRequestTest
    {
        [Fact]
        public async Task UpdateProject_Given_ValidName_ValidIsActive_ShouldReturn_OneRowAffected()
        {
            var guid = Guid.NewGuid();

            var createTestProject = new InsertProject(guid, TestString.Random(MaxLength.ProjectName));

            await _dataAccess.ExecuteAsync(createTestProject);

            var request = new UpdateProjectByGuid(guid, "UpdatedName", false);

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            Assert.Equal(1, rowsAffected);

            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(guid));
        }

        [Fact]
        public async Task UpdateProject_Given_NameNull_IsActiveNull_ShouldReturn_OneRowAffected_NoDataChanged()
        {
            var guid = Guid.NewGuid();

            var createTestProject = new InsertProject(guid, TestString.Random(MaxLength.ProjectName));

            await _dataAccess.ExecuteAsync(createTestProject);

            var originalProject = await _dataAccess.FetchAsync(new GetProjectByGuid(guid));

            var request = new UpdateProjectByGuid(guid, null, null);

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            var updatedProject = await _dataAccess.FetchAsync(new GetProjectByGuid(guid));

            Assert.Equal(1, rowsAffected);
            Assert.Equal(originalProject.Name, updatedProject.Name);
            Assert.Equal(originalProject.IsActive, updatedProject.IsActive);

            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(guid));
        }
    }
}

