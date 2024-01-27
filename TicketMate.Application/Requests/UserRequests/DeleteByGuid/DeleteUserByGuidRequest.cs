namespace TicketMate.Application.Requests.UserRequests.DeleteByGuid
{
    public class DeleteUserByGuidRequest : RequiredUserGuidRequest, IRequest
    {
        public DeleteUserByGuidRequest() { }

        public DeleteUserByGuidRequest(Guid userGuid) : base(userGuid) { }
    }
}
