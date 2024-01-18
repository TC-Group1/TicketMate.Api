using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.BaseRequests
{
    public abstract class IdDataRequest : IDataRequest
    {
        public int Id { get; set; }

        public IdDataRequest(int id) => Id = id;


        public abstract string GetSql();
        

        public object? GetParameters() => new { Id };

        
       
    }
}
