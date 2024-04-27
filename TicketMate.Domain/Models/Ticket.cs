namespace TicketMate.Domain.Models
{
    public class Ticket
    {
        public Ticket() { }

        public Ticket(
            Guid guid,
            Guid projectGuid, 
            string title,
            string description,
            string priorityId,
            string statusId,
            Guid createdByUserId)
        {
            Guid = guid;
            ProjectGuid = projectGuid;
            Title = title;
            Description = description;
            Priority = priorityId;
            Status = statusId;
            CreatedByUserId = createdByUserId;
        }
        public Guid Guid { get; set; }
        public Guid ProjectGuid { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
