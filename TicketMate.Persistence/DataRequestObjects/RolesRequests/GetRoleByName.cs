using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.RolesRequests
{
    public class GetRoleByName : IDataFetch<Roles_DTO>
    {
        public GetRoleByName(string name) => Name = name;
        public string Name { get; set; }
        public object? GetParameters() => this;
        public string GetSql() => $"SELECT * FROM {DatabaseTable.Roles} WHERE NAME LIKE @name";
        
    }
}
