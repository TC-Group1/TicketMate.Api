namespace TicketMate.Persistence.DataRequestObjects.UserRequests
{
    public class GetUserByGuid : BaseDataRequests.GuidDataRequest, IDataFetch<Users_DTO>
    {
        public GetUserByGuid(Guid guid) : base(guid) { }

        public object? GetParameters() => this;

        public override string GetSql() => $"SELECT * FROM {DatabaseTable.Users} WHERE Guid = @Guid";
    }
}
