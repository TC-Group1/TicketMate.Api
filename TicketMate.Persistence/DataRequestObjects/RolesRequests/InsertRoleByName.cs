namespace TicketMate.Persistence.DataRequestObjects.RolesRequests
{
    public class InsertRoleByName : IDataExecute
    {
        public InsertRoleByName(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"INSERT INTO {DatabaseTable.Roles} (Name) VALUES (@Name)";
    }
}
