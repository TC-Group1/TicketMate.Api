using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.ProjectRequests
{
    public class DeleteProjectByGuid : IDataExecute
    {
        public DeleteProjectByGuid(Guid projectGuid) => Guid = projectGuid;

        public Guid Guid { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"DELETE FROM {DatabaseTable.Projects} WHERE Guid = @Guid";
    }
}