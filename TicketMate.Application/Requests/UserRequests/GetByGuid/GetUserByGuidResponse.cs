namespace TicketMate.Application.Requests.UserRequests.GetByGuid
{
    public class GetUserByGuidResponse
    {
        public GetUserByGuidResponse(User user) => User = user;

        public User User { get; set; }
    }
}