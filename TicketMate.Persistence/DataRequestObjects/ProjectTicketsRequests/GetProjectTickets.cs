namespace TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests
{
    public class GetProjectTickets : IDataFetch<Tickets_DTO>
    {
        public GetProjectTickets(int projectId)
        {
            ProjectId = projectId;
        }
        public int ProjectId { get; set; }

        public object? GetParameters() => this;

        public string GetSql() =>
            $@"
                SELECT {DatabaseTable.Tickets}.* 
                FROM {DatabaseTable.Tickets}
                JOIN {DatabaseTable.ProjectTickets} ON {DatabaseTable.Tickets}.Id={DatabaseTable.ProjectTickets}.TicketId 
                WHERE {DatabaseTable.ProjectTickets}.ProjectId=@ProjectId;
            ";
    }
}
