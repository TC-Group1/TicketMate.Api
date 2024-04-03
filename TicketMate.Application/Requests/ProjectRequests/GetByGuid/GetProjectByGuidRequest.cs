using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Application.Requests.ProjectRequests.GetByGuid
{
    public class GetProjectByGuidRequest : IRequestResponse<GetProjectByGuidResponse>, IValidatable
    {
        public GetProjectByGuidRequest() { }

        public GetProjectByGuidRequest(Guid projectGuid) => ProjectGuid = projectGuid;

        public Guid ProjectGuid { get; set; }

        public bool IsValid(out Validator validator)
        {
            validator = new(new GuidRequiredRule(ProjectGuid, nameof(ProjectGuid)));

            return validator.IsPassingAllRules;
        }
    }
}
