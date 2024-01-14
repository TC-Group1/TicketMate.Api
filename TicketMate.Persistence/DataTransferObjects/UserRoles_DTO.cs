using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataTransferObjects
{
    public class UserRoles_DTO
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
