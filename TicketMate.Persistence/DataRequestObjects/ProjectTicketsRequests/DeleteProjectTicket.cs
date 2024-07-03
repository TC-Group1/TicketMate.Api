namespace TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests
{
	public class DeleteProjectTicket : IDataExecute
	{
		public DeleteProjectTicket(int projectId, int ticketId)
		{
			ProjectId = projectId;
			TicketId = ticketId;
		}

		public int ProjectId { get; set; }
		public int TicketId { get; set; }

		public object? GetParameters() => this;

		public string GetSql() => $@"DELETE FROM {DatabaseTable.ProjectTickets} WHERE ProjectId = @ProjectId AND TicketId = @TicketId";

	}
}
