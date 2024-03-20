using TicketMate.Domain.Constants;
using TicketMate.Domain.Validation.GuidValidation;
using TicketMate.Domain.Validation.StringValidation;

namespace TicketMate.Application.Requests.UserRequests.Insert
{
    public class InsertUserRequest : IValidatable, IRequest
    {
        #region Constructor

        public InsertUserRequest(Guid guid, string firstName, string lastName, string phoneNumber, string email, string avatar, int isActive, string passwordHash)
        {
            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Avatar = avatar;
            IsActive = isActive;
            PasswordHash = passwordHash;
        }

        #endregion

        #region Public Properties

        public Guid Guid { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public int IsActive { get; set; }
        public string PasswordHash { get; set; } = null!;

        #endregion

        #region IValidatable Validation

        public bool IsValid(out Validator validator)
        {
            validator = new(
                new GuidRequiredRule(Guid, nameof(Guid)),
                new StringLengthLimitRule(FirstName, nameof(FirstName), MaxLength.FirstName),
                new StringLengthLimitRule(LastName, nameof(LastName), MaxLength.LastName),
                new StringLengthLimitRule(PhoneNumber, nameof(PhoneNumber), MaxLength.PhoneNumber),
                new StringLengthLimitRule(Email, nameof(Email), MaxLength.Email),
                new StringLengthLimitRule(Avatar, nameof(Avatar), MaxLength.Avatar),
                new StringLengthLimitRule(PasswordHash, nameof(PasswordHash), MaxLength.PasswordHash)
                ); 

            return validator.IsPassingAllRules;
        }

        #endregion
    }
}
