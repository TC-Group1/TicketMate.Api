using TicketMate.Domain.Constants;
using TicketMate.Domain.Validation.GuidValidation;
using TicketMate.Domain.Validation.StringValidation;

namespace TicketMate.Application.Requests.UserRequests.Insert
{
    public class InsertUserRequest : IValidatable, IRequest
    {
        public InsertUserRequest(Guid guid, string username, string passwordHash)
        {
            Guid = guid;
            Username = username;
            PasswordHash = passwordHash;
        }

        public InsertUserRequest()
        {

        }
        public Guid Guid { get; set; }

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public bool IsValid(out Validator validator)
        {
            validator = new(
                new GuidRequiredRule(Guid, nameof(Guid)),
                new StringLengthLimitRule(Username, nameof(Username), MaxLength.Username),
                new StringLengthLimitRule(PasswordHash, nameof(PasswordHash), MaxLength.PasswordHash)
                );

            return validator.IsPassingAllRules;
        }
    }
}
