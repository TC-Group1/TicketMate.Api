using TicketMate.Domain.Extensions;

namespace TicketMate.Persistence.DataRequestObjects.RolesRequests
{
    public class InsertRoleByName : IDataExecute
    {

        private string _name = string.Empty;

        public string Name { get { return _name; } private set { _name = value.LowerAndTrim(); } }

        public InsertRoleByName(string name) => Name = name;

        public object? GetParameters() => new { Name };

        public string GetSql() => $"INSERT INTO {DatabaseTable.Roles} (Name) VALUES (@Name)";
    }
}
