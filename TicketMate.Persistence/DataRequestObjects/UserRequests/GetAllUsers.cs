namespace TicketMate.Persistence.DataRequestObjects.UserRequests
{
    public class GetAllUsers : IDataFetch<Users_DTO>
    {
        public object? GetParameters() => null;

        public string GetSql() => $"SELECT * FROM {DatabaseTable.Users}";
    }
}
