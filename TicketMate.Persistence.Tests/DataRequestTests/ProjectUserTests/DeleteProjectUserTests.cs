using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests;
using TicketMate.Persistence.DataRequestObjects.ProjectUserRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.Projects;
using TicketMate.Tests.Shared.Tickets;
using TicketMate.Tests.Shared.Users;

namespace TicketMate.Persistence.Tests.DataRequestTests.ProjectUserTests;

public class DeleteProjectUserTests : BaseDataRequestTest
{
    [Fact]
    public async Task DeleteProjectUser_Given_ProjectUserIsDeleted_ShouldReturn_OneRowAffected()
    {
        //Get Existing Data
        var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();
        var existingProject = await TestProject.InsertAndFetchProjectDtoAsync();
        
        //Insert a New Project User
        await _dataAccess.ExecuteAsync(new InsertProjectUser(existingProject.Guid, existingUser.Guid));

        //Delete the Project User Created
        var rowsAffectedWhenDeleting =
            await _dataAccess.ExecuteAsync(new DeleteProjectUser(existingProject.Guid, existingUser.Guid));
        
        //Delete the Data Used to Create a New Project User
        await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(existingUser.Guid));
        await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingProject.Guid));

        //Assert.NotNull(projectUserBeforeDeleting);
        Assert.Equal(1, rowsAffectedWhenDeleting);
    } 
    
} 