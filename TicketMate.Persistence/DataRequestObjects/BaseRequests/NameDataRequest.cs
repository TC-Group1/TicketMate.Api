using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.BaseRequests
{
    public abstract class NameDataRequest : IDataRequest
    {

        public string Name { get; set; }
        public NameDataRequest(string name) => Name = name;
        public object? GetParameters() => new { Name };
        public abstract string GetSql();
        
    }
}
