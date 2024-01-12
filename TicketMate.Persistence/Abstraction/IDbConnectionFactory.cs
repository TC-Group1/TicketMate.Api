using System.Data;

namespace TicketMate.Persistence.Abstraction
{
    public interface IDbConnectionFactory
    {
        public IDbConnection NewConnection();
    }
}
