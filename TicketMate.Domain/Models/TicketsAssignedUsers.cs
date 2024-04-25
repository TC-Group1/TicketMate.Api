namespace TicketMate.Domain.Models
{
    public class TicketsAssignedUsers
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
    }
}
