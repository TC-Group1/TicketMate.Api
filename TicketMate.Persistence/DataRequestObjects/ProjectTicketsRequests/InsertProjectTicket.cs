namespace TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests
{
	public class InsertProjectTicket : IDataExecute
	{
		public InsertProjectTicket(int projectId, int ticketId)
		{
			ProjectId = projectId;
			TicketId = ticketId;
		}

		public int ProjectId { get; set; }
		public int TicketId { get; set; }

		public object? GetParameters() => this;
	
		public string GetSql() => $@"INSERT INTO {DatabaseTable.ProjectTickets} (ProjectId, TicketId) VALUES (@ProjectId, @TicketId)";
	}
}
