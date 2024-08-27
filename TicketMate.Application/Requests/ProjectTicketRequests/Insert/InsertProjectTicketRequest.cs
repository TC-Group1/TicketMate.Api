using TicketMate.Domain.Constants;
using TicketMate.Domain.Validation.GuidValidation;
using TicketMate.Domain.Validation.StringValidation;

namespace TicketMate.Application.Requests.ProjectTicketRequests.Insert
{
    public class InsertProjectTicketRequest : IValidatable, IRequest
    {
        public InsertProjectTicketRequest() { }

        public Guid TicketGuid { get; set; }
        public Guid ProjectGuid { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? PriorityId { get; set; }
        public int StatusId { get; set; } = 1;
        public Guid CreatedByUserGuid { get; set; }

        public bool IsValid(out Validator validator)
        {
            validator = new();

            validator.ApplyRule(new GuidRequiredRule(TicketGuid, nameof(TicketGuid)));
            validator.ApplyRule(new GuidRequiredRule(ProjectGuid, nameof(ProjectGuid)));
            validator.ApplyRule(new GuidRequiredRule(CreatedByUserGuid, nameof(CreatedByUserGuid)));

            validator.ApplyRule(new StringLengthLimitRule(Title, nameof(Title), MaxLength.TicketName));

            return validator.IsPassingAllRules;
        }
    }
}
