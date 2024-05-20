using Microsoft.AspNetCore.Mvc;
using TicketMate.Application.Abstraction;
using TicketMate.Application.Requests.UserRequests.DeleteByGuid;
using TicketMate.Application.Requests.UserRequests.GetByGuid;
using TicketMate.Application.Requests.UserRequests.Insert;
using TicketMate.Application.Requests.UserRequests.UpdateByGuid;

namespace TicketMate.Api.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IOrchestrator orchestrator) : base(orchestrator) 
        {

        }

        [HttpDelete("User/DeleteUserByGuid")]
        public async Task DeleteUserByGuid(DeleteUserByGuidRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("User/GetUserByGuid")]
        public async Task<GetUserByGuidResponse> GetUserByGuid(GetUserByGuidRequest request) => await _orchestrator.GetRequestResponseAsync(request);

        [HttpPost("User/InsertUser")]
        public async Task InsertUser(InsertUserRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpPut("User/UpdateUserByGuid")]
        public async Task UpdateUserByGuid([FromQuery]UpdateUserByGuidRequest request) => await _orchestrator.ExecuteRequestAsync(request);
    }
}
