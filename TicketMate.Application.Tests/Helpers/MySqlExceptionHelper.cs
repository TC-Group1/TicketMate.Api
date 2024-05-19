using MySql.Data.MySqlClient;
using System.Reflection;

namespace TicketMate.Application.Tests.Helpers
{
    public class MySqlExceptionHelper
    {
        public static MySqlException Instantiate(string message = "", int number = 0)
        {
            var exception = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(MySqlException)) as MySqlException;

            typeof(MySqlException).GetField("_message", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(exception, message);
            typeof(MySqlException).GetField("Number", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(exception, number);

            return exception!;
        }
    }
}
