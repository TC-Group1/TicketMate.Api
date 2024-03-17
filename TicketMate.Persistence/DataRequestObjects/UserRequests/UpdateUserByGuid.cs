using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataRequestObjects.UserRequests
{
    public class UpdateUserByGuid :  IDataExecute
    {
        public UpdateUserByGuid(Guid guid, string firstName, string lastName, string phoneNumber, string email, string avatar, int isActive, string passwordHash)
        {
            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Avatar = avatar;
            IsActive = isActive;
            PasswordHash = passwordHash;
        }

        public Guid Guid { get; set; }

        public string? PasswordHash { get; set; }

        public string? Email { get; set; }

        public string? Avatar { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public int? IsActive { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $@"UPDATE {DatabaseTable.Users}  
                                           SET PASSWORDHASH = COALESCE(@passwordHash, PASSWORDHASH),  
                                           EMAIL = COALESCE(@email, EMAIL), 
                                           AVATAR = COALESCE(@avatar, AVATAR), 
                                           FIRSTNAME = COALESCE(@firstName, FIRSTNAME), 
                                           LASTNAME = COALESCE(@lastName, LASTNAME), 
                                           PHONENUMBER = COALESCE(@phoneNumber, PHONENUMBER), 
                                           ISACTIVE = COALESCE(@isActive, ISACTIVE) 
                                           WHERE GUID = @guid";

    }
}
