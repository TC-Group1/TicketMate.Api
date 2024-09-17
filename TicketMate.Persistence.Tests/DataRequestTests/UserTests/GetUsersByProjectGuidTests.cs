using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.Projects;
using TicketMate.Tests.Shared.Users;

namespace TicketMate.Persistence.Tests.DataRequestTests.UserTests;

public class GetUsersByProjectGuidTests : BaseDataRequestTest
{
    [Fact]
    public async Task GetUsersByProjectGuid_Given_ProjectExists_ShouldReturn_ProjectUsers_DTO()
    {
        var project_DTO = await TestProject.InsertAndFetchProjectDtoAsync();

        var existingUserOne = await TestUser.InsertAndFetchUsersDtoAsync();

        var results = await _dataAccess.FetchListAsync(new GetUsersByProjectGuid(project_DTO.Guid));

        var userOne = results.Single(_ => _.Guid == existingUserOne.Guid);

        Assert.Equal(existingUserOne.Guid, userOne.Guid);
        Assert.Equal(existingUserOne.PhoneNumber, userOne.PhoneNumber);
        Assert.Equal(existingUserOne.FirstName, userOne.FirstName);
        Assert.Equal(existingUserOne.LastName, userOne.LastName);
        Assert.Equal(existingUserOne.Email, userOne.Email);
        Assert.Equal(existingUserOne.Avatar, userOne.Avatar);
        Assert.Equal(existingUserOne.IsActive, userOne.IsActive);
        Assert.Equal(existingUserOne.PasswordHash, userOne.PasswordHash);
    }
}