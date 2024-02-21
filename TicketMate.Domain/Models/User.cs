namespace TicketMate.Domain.Models
{
    public class User
    {
        public User() { }

        public User(Guid guid, int phoneNumber, string firstName,string lastName, string email, string avatar, string passwordHash)
        {
            Guid = guid;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;  
            Email = email;
            Avatar = avatar;
            PasswordHash = passwordHash;
        }

        public Guid Guid { get; set; }
        public int PhoneNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
