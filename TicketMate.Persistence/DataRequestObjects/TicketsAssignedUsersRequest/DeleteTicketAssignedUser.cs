namespace TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest
{
    public class DeleteTicketAssignedUser : IDataExecute
    {
        public DeleteTicketAssignedUser(int userId, int ticketId)
        {
            UserId = userId;
            TicketId = ticketId;
        }

        public int UserId { get; set; }
        public int TicketId { get; set; }

        public string GetSql() => $"DELETE FROM {DatabaseTable.TicketsAssignedUsers} WHERE UserId = @UserId AND TicketId = @TicketId";

        public object? GetParameters() => this;

    }
}
