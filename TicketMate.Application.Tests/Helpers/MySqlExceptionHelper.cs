using MySql.Data.MySqlClient;
using System.Reflection;

namespace TicketMate.Application.Tests.Helpers
{
    public class MySqlExceptionHelper
    {
        public static MySqlException Instantiate(string message)
        {
            var exception = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(MySqlException)) as MySqlException;

            typeof(MySqlException).GetField("_message", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(exception, message);

            return exception!;
        }
    }
}
