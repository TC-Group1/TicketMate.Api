using Microsoft.AspNetCore.Mvc;
using TicketMate.Application.Abstraction;
using TicketMate.Application.Requests.ProjectRequests.Insert;
using TicketMate.Application.Requests.ProjectRequests.GetByGuid;
using TicketMate.Application.Requests.ProjectRequests.UpdateByGuid;

namespace TicketMate.Api.Controllers
{
    public class ProjectController : BaseController
    {
        public ProjectController(IOrchestrator orchestrator) : base(orchestrator)
        {
        }

        [HttpPost("Project/InsertProject")]
        public async Task InsertProject(InsertProjectRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("Project/GetProjectByGuid")]
        public async Task GetByGuid(GetProjectByGuidRequest request) => await _orchestrator.GetRequestResponseAsync(request);

        [HttpPut("Project/UpdateProjectByGuid")]
        public async Task UpdateProjectByGuid([FromQuery] UpdateProjectByGuidRequest request) => await _orchestrator.ExecuteRequestAsync(request);
    }
}
