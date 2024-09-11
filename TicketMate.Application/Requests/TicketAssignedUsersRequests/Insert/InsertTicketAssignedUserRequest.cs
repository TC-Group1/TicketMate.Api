using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Application.Requests.TicketAssignedUsersRequests.Insert
{
	public class InsertTicketAssignedUserRequest : IValidatable, IRequest
	{
		#region Constructors

		public InsertTicketAssignedUserRequest()
		{
		}

		public InsertTicketAssignedUserRequest(Guid ticketGuid, Guid userGuid)
		{
			TicketGuid = ticketGuid;
			UserGuid = userGuid;
		}

		#endregion

		#region Public Properties

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

		#endregion

	}
}
