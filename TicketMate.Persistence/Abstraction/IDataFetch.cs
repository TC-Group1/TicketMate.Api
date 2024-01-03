namespace TicketMate.Domain.Interfaces
{
    public interface IDataFetch<T> where T : class
    {
        public string GetSql();

        public object? GetParameters();
    }
}
