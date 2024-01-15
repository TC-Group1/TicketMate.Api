using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.BaseRequests
{
    public abstract class IdDataRequest : IDataRequest
    {
        //public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public IdDataRequest(int userId, int roleId) 
        { 
            UserId = userId;
            RoleId = roleId;
            //Id = id;
        }


        public abstract string GetSql();
        

        public object? GetParameters() => this;

        
       
    }
}
