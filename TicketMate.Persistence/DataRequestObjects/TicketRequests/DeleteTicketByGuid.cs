namespace TicketMate.Persistence.DataRequestObjects.TicketRequests
{
    /// <summary>
    /// Delete Ticket By Guid Request
    /// </summary>
    public class DeleteTicketByGuid : GuidDataRequest, IDataExecute
    {
        public DeleteTicketByGuid(Guid guid) : base(guid) { }

        public override string GetSql() => $"DELETE FROM {DatabaseTable.Tickets} WHERE Guid = @Guid";
    }
}
