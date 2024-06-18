using TicketMate.Domain.Constants;
using TicketMate.Domain.Validation.GuidValidation;
using TicketMate.Domain.Validation.StringValidation;

namespace TicketMate.Application.Requests.TicketRequests.UpdateByGuid
{
    public class UpdateTicketByGuidRequest : IValidatable, IRequest
    {
        public UpdateTicketByGuidRequest() { }
        public Guid Guid { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public int? PriorityId { get; set; }
        public int StatusId { get; set; }
        public bool IsValid(out Validator validator)
        {
            validator = new();

            validator.ApplyRule(new GuidRequiredRule(Guid, nameof(Guid)));
            validator.ApplyRule(new StringRequiredRule(Title, nameof(Title)));
            validator.ApplyRule(new StringLengthLimitRule(Title, nameof(Title), MaxLength.TicketName));

            return validator.IsPassingAllRules;
        }
    }
}
