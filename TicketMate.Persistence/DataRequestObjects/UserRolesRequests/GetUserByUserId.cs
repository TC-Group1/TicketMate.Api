using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.UserRolesRequests
{
    public class GetUserByUserId : IDataFetch<UserRoles_DTO>
    {
        public GetUserByUserId(int userId) => UserId = userId;
        
        public int UserId { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"SELECT * FROM {DatabaseTable.UserRoles} WHERE USERID = @userId";        
    }
}
