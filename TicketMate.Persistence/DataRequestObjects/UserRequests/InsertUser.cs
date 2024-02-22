namespace TicketMate.Persistence.DataRequestObjects.UserRequests
{
    public class InsertUser : IDataExecute
    {
        public InsertUser(Guid guid, string firstName, string lastName, string phoneNumber, string email, string avatar, int isActive, string passwordHash)
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

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public int IsActive { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"INSERT INTO {DatabaseTable.Users} (Guid, FirstName, LastName, PhoneNumber, Email, Avatar, IsActive, PasswordHash) " +
            $"                      VALUES (@Guid, @FirstName, @LastName, @PhoneNumber, @Email, @Avatar, @IsActive, @PasswordHash)";
    }
}