namespace TicketMate.Persistence.DataRequestObjects.UserRequests
{
    public class InsertUser : IDataExecute
    {
        public InsertUser(Guid guid, string userName, string passwordHash)
        {
            Guid = guid;
            Username = userName;
            PasswordHash = passwordHash;
        }

        public Guid Guid { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"INSERT INTO {DatabaseTable.Users} (Guid, Username, PasswordHash) VALUES (@Guid, @Username, @PasswordHash)";
    }
}