using TicketMate.Application.Abstraction;

namespace TicketMate.Api.Controllers
{
    public abstract class BaseController
    {
        protected readonly IOrchestrator _orchestrator;

        public BaseController(IOrchestrator orchestrator) => _orchestrator = orchestrator;
    }
}
