using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.UserRequests
{
    public class UpdateUserByGuid : UserBaseDataRequest, IDataExecute
    {
        public UpdateUserByGuid(
            Guid guid,
            string passwordHash,
            string email,
            string avatar,
            string firstName,
            string lastName,
            string phoneNumber,
            int isActive) 
            : base(
                guid,
                passwordHash,
                email,
                avatar,
                firstName,
                lastName,
                phoneNumber,
                isActive) { }
        
        public override string GetSql() => $"UPDATE {DatabaseTable.Users} " +
                                           $"SET PASSWORDHASH = @passwordHash, " +
                                           $"EMAIL = @email, " +
                                           $"AVATAR = @avatar, " +
                                           $"FIRSTNAME = @firstName, " +
                                           $"LASTNAME = @lastName, " +
                                           $"PHONENUMBER = @phoneNumber, " +
                                           $"ISACTIVE = @isActive " +
                                           $"WHERE GUID = @guid";
        
    }
}
