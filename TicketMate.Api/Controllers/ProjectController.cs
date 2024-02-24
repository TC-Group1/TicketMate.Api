using Microsoft.AspNetCore.Mvc;
using TicketMate.Application.Abstraction;
using TicketMate.Application.Requests.ProjectRequests.Insert;

namespace TicketMate.Api.Controllers
{
    public class ProjectController : BaseController
    {
        public ProjectController(IOrchestrator orchestrator) : base(orchestrator)
        {
        }

        [HttpPost("Project/InsertProject")]
        public async Task InsertProject(InsertProjectRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        
    }
}
