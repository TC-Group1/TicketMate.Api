namespace TicketMate.Application.Requests.UserRequests.GetByGuid
{
    public class GetUserByGuidRequest : RequiredUserGuidRequest, IRequestResponse<GetUserByGuidResponse>
    {
        public GetUserByGuidRequest() { }

        public GetUserByGuidRequest(Guid userGuid) : base(userGuid) { }
    }
}
