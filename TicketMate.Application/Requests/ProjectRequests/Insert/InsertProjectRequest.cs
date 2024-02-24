using TicketMate.Domain.Constants;
using TicketMate.Domain.Validation.GuidValidation;
using TicketMate.Domain.Validation.StringValidation;

namespace TicketMate.Application.Requests.ProjectRequests.Insert
{
    public class InsertProjectRequest : IValidatable, IRequest
    {
        public InsertProjectRequest(Guid guid, string name)
        {
            Guid = guid;
            Name = name;
        }

        public InsertProjectRequest()
        {
            
        }

        public Guid Guid { get; set; }

        public string Name { get; set; } = null!;

        public bool IsValid(out Validator validator)
        {
            validator = new();

            validator.ApplyRule(new GuidRequiredRule(Guid, nameof(Guid)));

            validator.ApplyRule(new StringLengthLimitRule(Name, nameof(Name), MaxLength.ProjectName));

            return validator.IsPassingAllRules;
        }
    }
}
