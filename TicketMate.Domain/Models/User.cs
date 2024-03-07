namespace TicketMate.Domain.Models
{
    public class User
    {
        public User() { }

        public User(Guid guid, string phoneNumber, string firstName,string lastName, string email, string avatar, string passwordHash, int isActive)
        {
            Guid = guid;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;  
            Email = email;
            Avatar = avatar;
            PasswordHash = passwordHash;
            IsActive = isActive;
        }

        public Guid Guid { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int IsActive { get; set; }
    }
}
