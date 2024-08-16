using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Application.Requests.TicketRequests.DeleteByGuid
{
    public class DeleteProjectTicketRequest : IValidatable, IRequest
    {
        public DeleteProjectTicketRequest() { }

        public DeleteProjectTicketRequest(Guid ticketGuid) => TicketGuid = ticketGuid;

        public Guid TicketGuid { get; set; }
        public bool IsValid(out Validator validator)
        {
            validator = new(new GuidRequiredRule(TicketGuid, nameof(TicketGuid)));

            return validator.IsPassingAllRules;
        }
    }
}
