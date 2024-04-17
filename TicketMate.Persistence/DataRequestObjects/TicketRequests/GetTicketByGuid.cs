
namespace TicketMate.Persistence.DataRequestObjects.TicketRequests
{
    public class GetTicketByGuid : GuidDataRequest, IDataFetch<Tickets_DTO>
    {
        public GetTicketByGuid(Guid guid) : base(guid) { }

        public override string GetSql() => $"SELECT * FROM {DatabaseTable.Tickets} WHERE Guid = @Guid";

    }
}
