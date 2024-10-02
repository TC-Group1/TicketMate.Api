using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.ProjectUserRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.TestObjects;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserTests;

public class GetUsersByProjectGuidTests : BaseDataRequestTest
{
    [Fact]
    public async Task GetUsersByProjectGuid_Given_ProjectExists_ShouldReturn_Users()
    {
        var existingProject = await TestProject.InsertAndFetchProjectDtoAsync();

        var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();
        await _dataAccess.ExecuteAsync(new InsertProjectUser(existingProject.Guid, existingUser.Guid));

        var results = await _dataAccess.FetchListAsync(new GetUsersByProjectGuid(existingProject.Guid));

        var userOne = results.Single(_ => _.Guid == existingUser.Guid);

        await _dataAccess.ExecuteAsync(new DeleteProjectUser(existingProject.Guid, existingUser.Guid));
        await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));
        await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(existingProject.Guid));

        Assert.Equal(existingUser.Guid, userOne.Guid);
        Assert.Equal(existingUser.PhoneNumber, userOne.PhoneNumber);
        Assert.Equal(existingUser.FirstName, userOne.FirstName);
        Assert.Equal(existingUser.LastName, userOne.LastName);
        Assert.Equal(existingUser.Email, userOne.Email);
        Assert.Equal(existingUser.Avatar, userOne.Avatar);
        Assert.Equal(existingUser.IsActive, userOne.IsActive);
        Assert.Equal(existingUser.PasswordHash, userOne.PasswordHash);
    }
}