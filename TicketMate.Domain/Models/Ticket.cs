namespace TicketMate.Domain.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime DateUpdated { get; set; }

        public Ticket() { }

        public Ticket( Guid guid, int projectId, string title, string description, int priorityId, int statusId,int createdByUserId)
        {
            Guid = guid;
            ProjectId = projectId;
            Title = title;
            Description = description;
            PriorityId = priorityId;
            StatusId = statusId;
            CreatedByUserId = createdByUserId;
        }
    }
}
