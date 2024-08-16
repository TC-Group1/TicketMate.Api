namespace TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests
{
	public class DeleteProjectTicket : IDataExecute
	{
		public DeleteProjectTicket(Guid projectGuid, Guid ticketGuid)
		{
			ProjectGuid = projectGuid;
			TicketGuid = ticketGuid;
		}

		public Guid ProjectGuid { get; set; }
		public Guid TicketGuid { get; set; }

		public object? GetParameters() => this;

		public string GetSql() => 
		$@"
			DELETE FROM {DatabaseTable.ProjectTickets}
			WHERE TicketId = (SELECT Id FROM {DatabaseTable.Tickets} WHERE Guid = @TicketGuid)
			AND ProjectId = (SELECT Id FROM {DatabaseTable.Projects} WHERE Guid = @ProjectGuid)
		";

    }
}
