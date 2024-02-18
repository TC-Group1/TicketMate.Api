using TicketMate.Persistence.BaseDataRequests;

namespace TicketMate.Persistence.DataRequestObjects.UserRolesRequests
{
    public class InsertUserRole : UserIdRoleIdRequest, IDataExecute
    {
        public InsertUserRole(int userId, int roleId) : base(userId, roleId) { }

        public override string GetSql() => $"INSERT INTO {DatabaseTable.UserRoles} (RoleId, UserId) VALUES (@roleId, @userId)";
    }
}
