using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Application.BaseObjects.BaseRequests
{
    public abstract class RequiredUserGuidRequest : IValidatable
    {
        public RequiredUserGuidRequest() { }

        protected RequiredUserGuidRequest(Guid userGuid) => UserGuid = userGuid;

        public Guid UserGuid { get; set; }

        public bool IsValid(out Validator validator)
        {
            validator = new Validator();

            validator.ApplyRule(new GuidRequiredRule(UserGuid, nameof(UserGuid)));

            return validator.IsPassingAllRules;
        }
    }
}
