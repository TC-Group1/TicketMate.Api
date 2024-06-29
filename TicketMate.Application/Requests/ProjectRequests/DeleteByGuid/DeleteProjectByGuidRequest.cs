using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Application.Requests.ProjectRequests.DeleteByGuid
{
    public class DeleteProjectByGuidRequest : IValidatable, IRequest
    {
        public DeleteProjectByGuidRequest(Guid guid)
        {
            Guid = guid;
        }

        public DeleteProjectByGuidRequest() { }

        public Guid Guid { get; set; }

        public bool IsValid(out Validator validator)
        {
            validator = new();

            validator.ApplyRule(new GuidRequiredRule(Guid, nameof(Guid)));

            return validator.IsPassingAllRules;
        }
    }
}
