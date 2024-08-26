using Microsoft.AspNetCore.Mvc;
using TicketMate.Application.Abstraction;
using TicketMate.Application.Requests.UserRequests.DeleteByGuid;
using TicketMate.Application.Requests.UserRequests.GetAll;
using TicketMate.Application.Requests.UserRequests.GetByGuid;
using TicketMate.Application.Requests.UserRequests.Insert;
using TicketMate.Application.Requests.UserRequests.UpdateByGuid;

namespace TicketMate.Api.Controllers
{
    public class UserController : BaseController
    {
        #region Constructor

        public UserController(IOrchestrator orchestrator) : base(orchestrator) { }

        #endregion

        #region Endpoints

        [HttpPost("User/InsertUser")]
        public async Task InsertUser(InsertUserRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("User/GetAllUsers")]
        public async Task GetAllUsers() => await _orchestrator.GetRequestResponseAsync(new GetAllUsersRequest());

        [HttpGet("User/GetUserByGuid")]
        public async Task<GetUserByGuidResponse> GetUserByGuid(GetUserByGuidRequest request) => await _orchestrator.GetRequestResponseAsync(request);
        
        [HttpPut("User/UpdateUserByGuid")]
        public async Task UpdateUserByGuid([FromQuery]UpdateUserByGuidRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        [HttpDelete("User/DeleteUserByGuid")]
        public async Task DeleteUserByGuid(DeleteUserByGuidRequest request) => await _orchestrator.ExecuteRequestAsync(request);

        #endregion
    }
}
