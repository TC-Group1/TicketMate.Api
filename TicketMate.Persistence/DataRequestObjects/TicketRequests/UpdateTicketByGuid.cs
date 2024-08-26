using TicketMate.Domain.Enums;

namespace TicketMate.Persistence.DataRequestObjects.TicketRequests
{
    /// <summary>
    /// Update Ticket by Guid
    /// </summary>
    public class UpdateTicketByGuid : IDataExecute
    {
        public UpdateTicketByGuid(Guid guid, string title, string description, int? priorityId, Statuses statusId)
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
        public Statuses StatusId { get; set; }
        public object? GetParameters() => this;
        public string GetSql() => $@"UPDATE {DatabaseTable.Tickets}  
                                           SET Title = COALESCE(@Title, Title),  
                                           DESCRIPTION = COALESCE(@Description, Description),
                                           PRIORITYID = COALESCE(@PriorityId, PriorityId), 
                                           STATUSID = @StatusId, 
                                           LASTMODIFIED = UTC_DATE()
                                           WHERE Guid = @guid";
    }
}
