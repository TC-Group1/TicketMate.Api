using Microsoft.AspNetCore.Mvc;
using TicketMate.Application.Abstraction;
using TicketMate.Application.Requests.TicketRequests.DeleteByGuid;
using TicketMate.Application.Requests.TicketRequests.GetByGuid;
using TicketMate.Application.Requests.TicketRequests.Insert;
using TicketMate.Application.Requests.TicketRequests.UpdateByGuid;

namespace TicketMate.Api.Controllers
{
    public class TicketController : BaseController
    {
        public TicketController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("Ticket/InsertTicket")]
        public async Task InsertTicket(InsertTicketRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("Ticket/GetTicketByGuid")]
        public async Task GetByGuid(GetTicketByGuidRequest request) => await _orchestrator.GetRequestResponseAsync(request);

        [HttpPut("Ticket/UpdateTicketByGuid")]
        public async Task UpdateTicketByGuid([FromQuery] UpdateTicketByGuidRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpDelete("Ticket/DeleteTicketByGuid")]
        public async Task DeleteTicketByGuid(DeleteProjectTicketRequest request) => await _orchestrator.ExecuteRequestAsync(request);

    }
}
