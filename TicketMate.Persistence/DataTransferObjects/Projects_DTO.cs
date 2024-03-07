namespace TicketMate.Persistence.DataTransferObjects
{
    public class Projects_DTO
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
