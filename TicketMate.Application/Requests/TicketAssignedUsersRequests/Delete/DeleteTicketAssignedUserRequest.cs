using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Application.Requests.TicketAssignedUsersRequests.Delete
{
	public class DeleteTicketAssignedUserRequest : IValidatable, IRequest
	{
		public DeleteTicketAssignedUserRequest()
		{ 
		}

		public DeleteTicketAssignedUserRequest(Guid ticketGuid, Guid userGuid)
		{
			TicketGuid = ticketGuid;
			UserGuid = userGuid;
		}


		public Guid TicketGuid { get; set; }

		public Guid UserGuid { get; set; }

		public bool IsValid(out Validator validator)
		{
			validator = new(
				new GuidRequiredRule(TicketGuid, nameof(TicketGuid)),
				new GuidRequiredRule(UserGuid, nameof(UserGuid))
			);

			return validator.IsPassingAllRules;
		}
	}
}