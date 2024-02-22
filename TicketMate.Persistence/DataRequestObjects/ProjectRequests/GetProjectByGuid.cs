using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.ProjectRequests
{
    public class GetProjectByGuid : GuidDataRequest, IDataFetch<Projects_DTO>
    {
        public GetProjectByGuid(Guid guid) : base(guid) { }

        public override string GetSql() => $"SELECT * FROM {DatabaseTable.Projects} WHERE Guid = @Guid";
    }
}
