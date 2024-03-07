using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Application.BaseObjects.BaseRequests
{
    public abstract class RequiredUserGuidRequest : IValidatable
    {
        #region Constructors

        public RequiredUserGuidRequest() { }

        protected RequiredUserGuidRequest(Guid userGuid) => UserGuid = userGuid;

        #endregion

        #region Public Properties

        public Guid UserGuid { get; set; }

        #endregion

        #region IValidatable Validation

        public bool IsValid(out Validator validator)
        {
            validator = new(new GuidRequiredRule(UserGuid, nameof(UserGuid)));

            return validator.IsPassingAllRules;
        }

        #endregion
    }
}
