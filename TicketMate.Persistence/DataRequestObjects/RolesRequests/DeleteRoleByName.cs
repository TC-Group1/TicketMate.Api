﻿namespace TicketMate.Persistence.DataRequestObjects.RolesRequests
{
    public class DeleteRoleByName : NameDataRequest, IDataExecute
    {
        public DeleteRoleByName(string name) : base(name) { }

        public override string GetSql() => $"DELETE FROM {DatabaseTable.Roles} WHERE NAME = @Name";
    }
}
