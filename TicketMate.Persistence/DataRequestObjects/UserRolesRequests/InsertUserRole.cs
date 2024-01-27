namespace TicketMate.Persistence.DataRequestObjects.UserRolesRequests
{
    public class InsertUserRole : IdDataRequest, IDataExecute
    {
        public InsertUserRole(int userId, int roleId) : base(userId, roleId) { }

        public override string GetSql() => $"INSERT INTO {DatabaseTable.UserRoles} (ROLEID, USERID) VALUES (@roleId, @userId)";
    }
}
