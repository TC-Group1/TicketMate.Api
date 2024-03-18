using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.ProjectRequests
{
    public class UpdateProjectByGuid : IDataExecute
    {
        public Guid Guid { get; set; }

        public string? Name { get; set; }

        public bool? IsActive { get; set; }

        public UpdateProjectByGuid(Guid guid, string? name, bool? isActive) 
        {
            Guid = guid;

            if (name == "") name = null;
            Name = name;

            IsActive = isActive;
        }

        public object? GetParameters() => this;

        public string GetSql() => $"UPDATE {DatabaseTable.Projects} SET Name = COALESCE(@Name, Name), IsActive = COALESCE(@IsActive, IsActive) WHERE Guid = @guid;";
    }
}

