namespace TicketMate.Domain.Models
{
    public class Ticket
    {
        public Ticket() { }

        public Ticket(
            Guid guid, 
            string title,
            string description,
            DateTime dateCreated,
            DateTime? lastModified,
            int? priorityId = null,
            int statusId = 1)
        {
            Guid = guid;
            Title = title;
            Description = description;
            DateCreated = dateCreated;
            LastModified = lastModified;
            Priority = priorityId;
            Status = statusId;
        }
        public Guid Guid { get; set; }
        public Guid ProjectGuid { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? Priority { get; set; }
        public int Status { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
