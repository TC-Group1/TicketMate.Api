namespace TicketMate.Persistence.DataRequestObjects.UserRolesRequests
{
    public class DeleteUserRolesByUserId : IDataExecute
    {
        public DeleteUserRolesByUserId(int userId) => UserId = userId;

        public int UserId { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"DELETE FROM {DatabaseTable.UserRoles} WHERE UserId = @UserId";
    }
}
