using MySql.Data.MySqlClient;
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

            var request = new UpdateProjectByGuid(guid, TestString.Random(MaxLength.ProjectName), false);

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

        [Fact]
        public async Task UpdateProject_Given_NameEmptyString_ShouldReturn_OneRowAffected_NoDataChanged()
        {
            var guid = Guid.NewGuid();

            var createTestProject = new InsertProject(guid, TestString.Random(MaxLength.ProjectName));

            await _dataAccess.ExecuteAsync(createTestProject);

            var originalProject = await _dataAccess.FetchAsync(new GetProjectByGuid(guid));

            var request = new UpdateProjectByGuid(guid, "", true);

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            var updatedProject = await _dataAccess.FetchAsync(new GetProjectByGuid(guid));

            Assert.Equal(1, rowsAffected);
            Assert.Equal(originalProject.Name, updatedProject.Name);

            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(guid));
        }

        [Fact]
        public async Task UpdateProject_Given_NameExceedsMaxLength_ShouldThrow_MySqlException()
        {
            var guid = Guid.NewGuid();

            var createTestProject = new InsertProject(guid, TestString.Random(MaxLength.ProjectName));

            await _dataAccess.ExecuteAsync(createTestProject);

            var request = new UpdateProjectByGuid(guid, TestString.Random(MaxLength.ProjectName + 1), false);

            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            Assert.IsType<MySqlException>(exception);

            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(guid));
        }

        [Fact]
        public async Task UpdateProject_Given_ProjectGuidDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var guidThatDoesNotExist = Guid.NewGuid();

            var request = new UpdateProjectByGuid(guidThatDoesNotExist, TestString.Random(MaxLength.ProjectName), false);

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            Assert.Equal(0, rowsAffected);
        }
    }
}

