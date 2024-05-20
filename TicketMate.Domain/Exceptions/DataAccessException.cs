namespace TicketMate.Domain.Exceptions
{
    public class DataAccessException : Exception
    {
        public int ExceptionNumber { get; set; }

        public DataAccessException()
        {

        }

        public DataAccessException(string message, int exceptionNumber, Exception exception) : base(message, exception)
        {
            ExceptionNumber = exceptionNumber;
        }
    }
}
