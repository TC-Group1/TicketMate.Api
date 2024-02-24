using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Application.Requests.ProjectRequests.Insert;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;

namespace TicketMate.Application.Tests.RequestTests.ProjectRequestTests.InsertProjectTests
{
    public class InsertProjectHandlerTests : HandlerTest
    {
        private readonly InsertProjectRequest _request = new();

        private readonly InsertProjectHandler _handler;

        public InsertProjectHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task InsertProjectHandler_Given_ProjectIsInserted_ShouldReturn_TaskCompletedSuccessfully()
        {
            var rowsAffected = 1;

            SetupMockExecuteAsync<InsertProject>(rowsAffected);

            var task = _handler.ExecuteRequestAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }
    }
}
