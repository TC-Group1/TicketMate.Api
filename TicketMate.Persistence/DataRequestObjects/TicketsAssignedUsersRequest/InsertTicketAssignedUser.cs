namespace TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest
{
    public class InsertTicketAssignedUser : IDataExecute
    {
        public InsertTicketAssignedUser(int userId, int ticketId)
        {
            UserId = userId;
            TicketId = ticketId;
        }

        public int UserId { get; set; }
        public int TicketId { get; set; }

        public string GetSql() => $"INSERT INTO {DatabaseTable.TicketsAssignedUsers} (TicketId, UserId) VALUES (@TicketId, @UserId)";

        public object? GetParameters() => this;
    }
}
