namespace TicketMate.Persistence.DataTransferObjects
{
    public class Tickets_DTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? PriorityId { get; set; }
        public int StatusId { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? LastModified { get; set; }

    }
}
