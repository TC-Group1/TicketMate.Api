using Microsoft.AspNetCore.Mvc;
using TicketMate.Application.Abstraction;
using TicketMate.Application.Requests.TicketAssignedUsersRequests.Delete;
using TicketMate.Application.Requests.TicketAssignedUsersRequests.Insert;

namespace TicketMate.Api.Controllers
{
	public class TicketAssignedUserController : BaseController
	{
		public TicketAssignedUserController(IOrchestrator orchestrator) : base(orchestrator) { }

		[HttpPost("TicketAssignedUser/InsertTicketAssignedUser")]
		public async Task InsertTicketAssignedUser(InsertTicketAssignedUserRequest request) => await _orchestrator.ExecuteRequestAsync(request);

		[HttpDelete("TicketAssignedUser/DeleteTicketAssignedUser")]
		public async Task DeleteTicketAssignedUser(DeleteTicketAssignedUserRequest request) => await _orchestrator.ExecuteRequestAsync(request);
	}
}
