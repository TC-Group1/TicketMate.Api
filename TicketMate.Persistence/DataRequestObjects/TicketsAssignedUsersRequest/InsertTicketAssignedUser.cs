using TicketMate.Domain.Validation;

namespace TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest
{
    public class InsertTicketAssignedUser : IDataExecute
    {
        public InsertTicketAssignedUser(Guid userGuid, Guid ticketGuid)
        {
            UserGuid = userGuid;
            TicketGuid = ticketGuid;
        }

        public Guid UserGuid { get; set; }
        public Guid TicketGuid { get; set; }


		public string GetSql() => 
		$@"
			INSERT INTO {DatabaseTable.TicketsAssignedUsers} (TicketId, UserId)
			VALUES (
				(SELECT Id FROM {DatabaseTable.Tickets} WHERE Guid = @TicketGuid),
				(SELECT Id FROM {DatabaseTable.Users} WHERE Guid = @UserGuid)
			)
		";

		public object? GetParameters() => this;

	}
}
