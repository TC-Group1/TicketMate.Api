namespace TicketMate.Persistence.DataRequestObjects.ProjectRequests
{
    public class InsertProject : IDataExecute
    {
        public InsertProject(Guid guid, string name)
        {
            Guid = guid;
            Name = name;
        }
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $"INSERT INTO {DatabaseTable.Projects} (Guid, Name) VALUES (@Guid, @Name)";
    }
}
