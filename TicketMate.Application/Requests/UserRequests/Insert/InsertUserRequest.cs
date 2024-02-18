using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            validator = new();

            validator.ApplyRule(new GuidRequiredRule(Guid, nameof(Guid)));

            validator.ApplyRule(new StringLengthLimitRule(Username, nameof(Username), MaxLength.Username));

            validator.ApplyRule(new StringLengthLimitRule(PasswordHash, nameof(PasswordHash), MaxLength.PasswordHash));

            return validator.IsPassingAllRules;

        }
    }
}
