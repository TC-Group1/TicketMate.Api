namespace TicketMate.Persistence.DataRequestObjects.TicketRequests
{
    /// <summary>
    /// Insert Ticket Request
    /// </summary>
    public class InsertTicket : IDataExecute
    {
        public InsertTicket(
            Guid guid,
            Guid projectGuid,
            string title,
            string description,
            int? priorityId,
            Guid createdByUserGuid,
            int statusId = 1)
        {
            Guid = guid;
            ProjectGuid = projectGuid;
            Title = title;
            Description = description;
            PriorityId = priorityId;
            StatusId = statusId;
            CreatedByUserGuid = createdByUserGuid;
        }

        public Guid Guid { get; set; }
        public Guid ProjectGuid { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? PriorityId { get; set; }
        public int StatusId { get; set; } = 1;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public Guid CreatedByUserGuid { get; set; }
        public DateTime? DateUpdated { get; set; } = null;
        public object? GetParameters() => this;
        public string GetSql() => @$"INSERT INTO {DatabaseTable.Tickets} 
                                  (Guid, ProjectId, Title, Description, PriorityId, StatusId, CreatedByUserId) VALUES 
                                  (@Guid, (SELECT Id FROM {DatabaseTable.Projects} WHERE Guid = @ProjectGuid), @Title, @Description, @PriorityId, @StatusId, 
                                  (SELECT Id FROM {DatabaseTable.Users} WHERE Guid = @CreatedByUserGuid))";
    }
}
