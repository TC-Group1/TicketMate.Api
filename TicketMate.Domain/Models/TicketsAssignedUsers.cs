namespace TicketMate.Domain.Models
{
    public class TicketsAssignedUsers
    {
        public Guid Guid { get; set; }
        public Guid TicketGuid  { get; set; }
        public Guid UserGuid { get; set; }
    }
}
