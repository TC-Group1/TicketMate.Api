using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Application.Requests.TicketRequests.GetByGuid
{
    public class GetTicketByGuidRequest : IRequestResponse<GetTicketByGuidResponse>, IValidatable
    {
        public GetTicketByGuidRequest() { }

        public GetTicketByGuidRequest(Guid ticketGuid) => TicketGuid = ticketGuid;

        public Guid TicketGuid { get; set; }

        public bool IsValid(out Validator validator)
        {
            validator = new(new GuidRequiredRule(TicketGuid, nameof(TicketGuid)));

            return validator.IsPassingAllRules;
        }
    }
}
