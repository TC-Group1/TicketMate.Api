using TicketMate.Domain.Validation;
using TicketMate.Domain.Validation.GuidValidation;

namespace TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest
{
	public class DeleteTicketAssignedUser : IDataExecute, IValidatable
	{
		public DeleteTicketAssignedUser() { }
		public DeleteTicketAssignedUser(Guid userGuid, Guid ticketGuid)
		{
			UserGuid = userGuid;
			TicketGuid = ticketGuid;
		}

		public Guid UserGuid { get; set; }
		public Guid TicketGuid { get; set; }

		public object? GetParameters() => this;
		
		public string GetSql() =>
			$@"
                DELETE FROM {DatabaseTable.TicketsAssignedUsers}
                WHERE TicketId = (SELECT Id FROM {DatabaseTable.Tickets} WHERE Guid = @TicketGuid)
                AND UserId = (SELECT Id FROM {DatabaseTable.Users} WHERE Guid = @UserGuid)
            ";

		public bool IsValid(out Validator validator)
		{
			validator = new(new GuidRequiredRule(TicketGuid, nameof(TicketGuid)), new GuidRequiredRule(UserGuid, nameof(UserGuid)));

			return validator.IsPassingAllRules;
		}
	}
}
