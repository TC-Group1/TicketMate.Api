using TicketMate.Domain.Models;

namespace TicketMate.Persistence.DataTransferObjects
{
    public class Users_DTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int IsActive { get; set; }

        public User AsDomainUser() => new(Guid, PhoneNumber, FirstName, LastName, Email, Avatar, PasswordHash, IsActive);
    }
}
