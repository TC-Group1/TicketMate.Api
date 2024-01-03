namespace TicketMate.Domain.Interfaces
{
    public interface IDataExecute
    {
        public string GetSql();

        public object? GetParameters();
    }
}
