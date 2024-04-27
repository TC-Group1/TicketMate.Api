using Microsoft.AspNetCore.Mvc;
using TicketMate.Application.Abstraction;
using TicketMate.Application.Requests.ProjectRequests.Insert;
using TicketMate.Application.Requests.ProjectRequests.DeleteByGuid;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;

namespace TicketMate.Api.Controllers
{
    public class ProjectController : BaseController
    {
        public ProjectController(IOrchestrator orchestrator) : base(orchestrator)
        {
        }

        [HttpPost("Project/InsertProject")]
        public async Task InsertProject(InsertProjectRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpDelete("Project/DeleteProjectByGuid")]
        public async Task DeleteProjectByGuid(DeleteProjectByGuidRequest request) => await _orchestrator.ExecuteRequestAsync(request);
    }
}
