namespace TicketMate.Domain.Interfaces
{
    public interface IDataAccess
    {
        public Task<int> ExecuteAsync(IDataExecute request);

        public Task<TResponse?> FetchAsync<TResponse>(IDataFetch<TResponse> request) where TResponse : class;

        public Task<IEnumerable<TResponse>> FetchListAsync<TResponse>(IDataFetch<TResponse> request) where TResponse : class;
    }
}
