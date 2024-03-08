using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.BaseDataRequests
{
    public abstract class UserBaseDataRequest : IDataExecute
    {
        public Guid Guid { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public int IsActive { get; set; }

        public UserBaseDataRequest(
            Guid guid,
            string passwordHash,
            string email,
            string avatar,
            string firstName,
            string lastName,
            string phoneNumber,
            int isActive)  
        {
            Guid = guid; 
            PasswordHash = passwordHash; 
            Email = email; 
            Avatar = avatar;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
        }

        public object? GetParameters() => new {Guid, PasswordHash, Email, Avatar, FirstName, LastName, PhoneNumber, IsActive };

        public abstract string GetSql();
        
    }
}
