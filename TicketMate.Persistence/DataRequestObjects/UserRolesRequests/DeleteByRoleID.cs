using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.UserRolesRequests
{
    public class DeleteByRoleID : IdDataRequest, IDataExecute
    { 
        public DeleteByRoleID(int roleId, int userId) : base(userId, roleId) { }

        public override string GetSql() => $"DELETE FROM USERROLES WHERE ROLEID = @RoleId AND USERID = @UserId";
    }
}
