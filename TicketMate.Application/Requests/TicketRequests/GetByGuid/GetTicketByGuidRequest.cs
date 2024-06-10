using TicketMate.Application.Requests.ProjectRequests.GetByGuid;

namespace TicketMate.Application.Requests.TicketRequests.GetByGuid
{
    public class GetTicketByGuidRequest : IRequestResponse<GetTicketByGuidResponse>, IValidatable
    {
        public bool IsValid(out Validator validator)
        {
            throw new NotImplementedException();
        }
    }
}
