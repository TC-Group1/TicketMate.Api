using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Application.Requests.TicketRequests.DeleteByGuid
{
    public class DeleteTicketByGuidRequest : IValidatable, IRequest
    {
        public DeleteTicketByGuidRequest() { }

        public DeleteTicketByGuidRequest(Guid ticketGuid) => TicketGuid = ticketGuid;

        public Guid TicketGuid { get; set; }
        public bool IsValid(out Validator validator)
        {
            validator = new(new GuidRequiredRule(TicketGuid, nameof(TicketGuid)));

            return validator.IsPassingAllRules;
        }
    }
}
