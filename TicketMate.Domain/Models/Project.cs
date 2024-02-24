namespace TicketMate.Domain.Models
{
    public class Project
    {
        public Project() { }

        public Project(Guid guid, string name)
        {
            Guid = guid;
            Name = name;
        }

        public Guid Guid { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
