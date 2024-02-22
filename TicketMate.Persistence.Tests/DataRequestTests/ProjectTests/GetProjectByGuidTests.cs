using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;

namespace TicketMate.Persistence.Tests.DataRequestTests.ProjectTests
{
    public class GetProjectByGuidTests : BaseDataRequestTest
    {
        [Fact]
        public async Task GetProjectByGuid_ProjectNotExesting_ShouldReturn_Null()
        {
            Assert.Null(await _dataAccess.FetchAsync(new GetProjectByGuid(Guid.NewGuid())));
        }

        [Fact]
        public async Task GetProjectByGuid_Given_ProjectExists_ShouldReturn_Project_DTO()
        {
            var guid = Guid.NewGuid();

            var insertProjectRequest = new InsertProject(guid, "Name");

            await _dataAccess.ExecuteAsync(insertProjectRequest);

            var result = await _dataAccess.FetchAsync(new GetProjectByGuid(guid));

            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(guid));

            Assert.NotNull(result);
            Assert.Equal(insertProjectRequest.Guid, result.Guid);
            Assert.Equal(insertProjectRequest.Name, result.Name);
        }
    }
}
