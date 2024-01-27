namespace TicketMate.Persistence.DataRequestObjects.UserRolesRequests
{
    public class UpdateRoleIdByUserId : IdDataRequest, IDataExecute
    {
        public UpdateRoleIdByUserId(int userId, int roleId) : base(userId, roleId) { }

        public override string GetSql() => $"UPDATE {DatabaseTable.UserRoles} SET ROLEID = @roleId WHERE USERID = @userId";
    }
}
