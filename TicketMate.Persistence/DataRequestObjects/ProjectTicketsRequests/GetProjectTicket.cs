namespace TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests
{
    public class GetProjectTicket : IDataFetch<Tickets_DTO>
    {
        public GetProjectTicket(int projectId, int ticketId)
        {
            ProjectId = projectId;
            TicketId = ticketId;
        }
        public int ProjectId { get; set; }
        public int TicketId { get; set; }

        public object? GetParameters() => this;

        public string GetSql() =>
            $@"
                SELECT {DatabaseTable.Tickets}.* 
                FROM {DatabaseTable.Tickets}
                JOIN {DatabaseTable.ProjectTickets} ON {DatabaseTable.Tickets}.Id={DatabaseTable.ProjectTickets}.TicketId 
                WHERE ProjectId=@ProjectId;
            ";
    }
}
