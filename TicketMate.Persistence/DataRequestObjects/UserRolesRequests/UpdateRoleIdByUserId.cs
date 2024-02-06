namespace TicketMate.Persistence.DataRequestObjects.UserRolesRequests
{
    public class UpdateRoleIdByUserId : UserIdRoleIdRequest, IDataExecute
    {
        public UpdateRoleIdByUserId(int userId, int roleId) : base(userId, roleId) { }

        public override string GetSql() => $"UPDATE {DatabaseTable.UserRoles} SET ROLEID = @roleId WHERE USERID = @userId";
    }
}
