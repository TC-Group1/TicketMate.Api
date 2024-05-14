namespace TicketMate.Persistence.DataRequestObjects.TicketRequests
{
    /// <summary>
    /// Update Ticket by Guid
    /// </summary>
    public class UpdateTicketByGuid : IDataExecute
    {
        public UpdateTicketByGuid(Guid guid, string title, string description, int? priorityId, int? statusId)
        {
            Guid = guid;
            Title = title;
            Description = description;
            PriorityId = priorityId;
            StatusId = statusId;
        }
        public Guid Guid { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? PriorityId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? LastModified { get; set; } = DateTime.UtcNow;
        public object? GetParameters() => this;
        public string GetSql() => $@"UPDATE {DatabaseTable.Tickets}  
                                           SET Title = COALESCE(@Title, Title),  
                                           DESCRIPTION = COALESCE(@Description, Description),
                                           PRIORITYID = COALESCE(@PriorityId, PriorityId), 
                                           STATUSID = COALESCE(@StatusId, StatusId), 
                                           LASTMODIFIED = @LastModified
                                           WHERE Guid = @guid";
    }
}
