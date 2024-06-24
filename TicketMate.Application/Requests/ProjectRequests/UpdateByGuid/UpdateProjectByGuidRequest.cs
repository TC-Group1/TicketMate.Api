using TicketMate.Domain.Constants;
using TicketMate.Domain.Validation.GuidValidation;
using TicketMate.Domain.Validation.StringValidation;

namespace TicketMate.Application.Requests.ProjectRequests.UpdateByGuid
{
    public class UpdateProjectByGuidRequest : IValidatable, IRequest
    {
        public UpdateProjectByGuidRequest() { }
        public Guid Guid { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }


        public bool IsValid(out Validator validator)
        {
            validator = new();

            validator.ApplyRule(new GuidRequiredRule(Guid, nameof(Guid)));
            validator.ApplyRule(new StringRequiredRule(Name, nameof(Name)));
            validator.ApplyRule(new StringLengthLimitRule(Name, nameof(Name), MaxLength.ProjectName));

            return validator.IsPassingAllRules;
        }
    }
}
