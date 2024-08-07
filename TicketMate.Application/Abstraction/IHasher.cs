namespace TicketMate.Application.Abstraction
{
    public interface IHasher
    {
        byte[] GenerateHash(string input, byte[] salt);
        byte[] GenerateSalt();
        (byte[] hash, byte[] salt) HashInput(string input);
        bool VerifyHashedInput(string providedInput, byte[] salt, byte[] hash);
    }
}