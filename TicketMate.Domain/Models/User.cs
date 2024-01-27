namespace TicketMate.Domain.Models
{
    public class User
    {
        public User() { }

        public User(Guid guid, string username, string passwordHash)
        {
            Guid = guid;
            Username = username;
            PasswordHash = passwordHash;
        }

        public Guid Guid { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}
