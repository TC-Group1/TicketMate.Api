namespace TicketMate.Persistence.DataRequestObjects.UserRolesRequests
{
    public class GetUserRolesByUserId : IDataFetch<UserRoles_DTO>
    {
        public GetUserRolesByUserId(int userId) => UserId = userId;

        public int UserId { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"SELECT * FROM {DatabaseTable.UserRoles} JOIN Roles ON UserRoles WHERE UserId = @userId;";
    }
}
