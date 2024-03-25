namespace TicketMate.Persistence.Tests.DataRequestTests.Helpers
{
    public class RandomId
    {
        public static int Random(int start = 25, int end = 5000)
        {
            var random = new Random();
            return random.Next(start, end);
        }
    }
}
