using TicketMate.Domain.Models;

namespace TicketMate.Persistence.DataTransferObjects
{
    public class Users_DTO
    {
        public int Id { get; set; }
        
        public Guid Guid { get; set; }
        
        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public User AsDomainUser() => new(Guid, Username, PasswordHash);
    }
}
