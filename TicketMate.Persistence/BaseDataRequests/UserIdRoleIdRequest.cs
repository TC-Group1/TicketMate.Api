namespace TicketMate.Persistence.BaseDataRequests
{
    public abstract class UserIdRoleIdRequest : IDataRequest
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public UserIdRoleIdRequest(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
        public abstract string GetSql();

        public object? GetParameters() => this;
    }
}
