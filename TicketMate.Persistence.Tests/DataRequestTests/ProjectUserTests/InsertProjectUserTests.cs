using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests;
using TicketMate.Persistence.DataRequestObjects.ProjectUserRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.Helpers;
using TicketMate.Tests.Shared.TestObjects;



namespace TicketMate.Persistence.Tests.DataRequestTests.ProjectUserTests;

public class InsertProjectUserTests : BaseDataRequestTest
{
    [Fact]
    public async Task InsertProjectUser_Given_ProjectUserInserted_ShouldReturn_OneRowAffected()
    {
        var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();
        var existingProject = await TestProject.InsertAndFetchProjectDtoAsync();
        var request = new InsertProjectUser(existingProject.Guid, existingUser.Guid);

        var rowsAffected = await _dataAccess.ExecuteAsync(request);

        await _dataAccess.ExecuteAsync(new DeleteProjectUser(existingProject.Guid, existingUser.Guid));
        await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));
        await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(existingProject.Guid));

        Assert.Equal(1, rowsAffected);
    }
}