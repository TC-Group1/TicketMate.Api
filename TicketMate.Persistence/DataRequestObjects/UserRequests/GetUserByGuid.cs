namespace TicketMate.Persistence.DataRequestObjects.UserRequests
{
    public class GetUserByGuid : GuidDataRequest, IDataFetch<Users_DTO>
    {
        public GetUserByGuid(Guid guid) : base(guid) { }

        public override string GetSql() => $"SELECT * FROM {DatabaseTable.Users} WHERE Guid = @Guid";
    }
}
