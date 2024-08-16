using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Application.Requests.ProjectTicketRequests.Delete
{
    public class DeleteProjectTicketRequest : IValidatable, IRequest
    {
        #region Constructors

        public DeleteProjectTicketRequest() { }
        public DeleteProjectTicketRequest(Guid ticketGuid, Guid projectGuid)
        {
            TicketGuid = ticketGuid;
            ProjectGuid = projectGuid;
        }

        #endregion

        #region Public Properties

        public Guid TicketGuid { get; set; }
        public Guid ProjectGuid { get; set; }

        #endregion

        #region IValidatable Implementation

        public bool IsValid(out Validator validator)
        {
            validator = new Validator
            (
                new GuidRequiredRule(TicketGuid, nameof(TicketGuid)),
                new GuidRequiredRule(ProjectGuid, nameof(ProjectGuid))
            );

            return validator.IsPassingAllRules;
        }
        #endregion
    }
}
