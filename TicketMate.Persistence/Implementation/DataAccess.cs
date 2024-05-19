using Dapper;
using MySql.Data.MySqlClient;
using TicketMate.Domain.Exceptions;

namespace TicketMate.Persistence.Implementation
{
    internal class DataAccess : IDataAccess
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DataAccess(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public async Task<int> ExecuteAsync(IDataExecute request)
        {
            try
            {
                using var connection = _connectionFactory.NewConnection();

                connection.Open();

                return await connection.ExecuteAsync(request.GetSql(), request.GetParameters());
            }
            catch (MySqlException ex)
            {
                throw new DataAccessException(ex.Message, ex.Number, ex);
            }
        }

        public async Task<TResponse?> FetchAsync<TResponse>(IDataFetch<TResponse> request)
        {
            try
            {
                using var connection = _connectionFactory.NewConnection();

                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<TResponse>(request.GetSql(), request.GetParameters());
            }
            catch (MySqlException ex)
            {
                throw new DataAccessException(ex.Message, ex.Number, ex);
            }
        }

        public async Task<IEnumerable<TResponse>> FetchListAsync<TResponse>(IDataFetch<TResponse> request)
        {
            try
            {
                using var connection = _connectionFactory.NewConnection();

                connection.Open();

                return await connection.QueryAsync<TResponse>(request.GetSql(), request.GetParameters());
            }
            catch (MySqlException ex)
            {
                throw new DataAccessException(ex.Message, ex.Number, ex);
            }
        }
    }
}
