using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;

namespace TicketMate.Persistence.Tests.DataRequestTests.ProjectTests
{
    public class InsertProjectTests : BaseDataRequestTest
    {
        [Fact]
        public async Task InsertProject_Given_ProjectIsInserted_ShouldReturn_OneRowAffected()
        {
            var guid = Guid.NewGuid();

            // creates request object that will insert project
            var request = new InsertProject(guid, "Name");

            // executes the request and stores rows affected
            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            // ensures rows affected is 1
            Assert.Equal(1, rowsAffected);

            // cleans up project for test by deleting it
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(guid));
        }

        [Fact]
        public async Task InsertProject_Given_GuidAlreadyTaken_ShouldThrow_MySqlException()
        {
            var guid = Guid.NewGuid();

            // insert project with Guid so it is already taken
            await _dataAccess.ExecuteAsync(new InsertProject(guid, "Name"));

            // create a request that is inserting a project with the guid we just used
            var request = new InsertProject(guid, "anotherName");

            // storing the exception when we run that request
            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            // asserting that the exception is in fact a mysql exception
            Assert.IsType<MySqlException>(exception);

            // cleaning up, deleting project
            await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(request.Guid));
        }
    }
}
