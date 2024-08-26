using System.Text;

namespace TicketMate.Tests.Shared.TestObjects
{
    /// <summary>
    /// Generate a Random Test String
    /// </summary>
    public class TestString
    {
        private const string _characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-=+_)(*&^%$#@!";

        public static string Random(int length = 36)
        {
            var random = new Random();

            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                var randomIndex = random.Next(0, _characters.Length);

                sb.Append(_characters[randomIndex]);
            }

            return sb.ToString();
        }
    }
}
