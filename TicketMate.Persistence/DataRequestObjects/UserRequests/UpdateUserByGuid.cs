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
            string firstName,
            string lastName,
            string phoneNumber,
            string email,
            string avatar,
            int isActive,
            string passwordHash)
            : base(
                guid,
                firstName,
                lastName,
                phoneNumber,
                email,
                avatar,
                isActive,
                passwordHash)
        { }
                            

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
