using System.Security.Cryptography;
using System.Text;

namespace TicketMate.Application.Implementation
{
    /// <summary>
    /// Service Class used for Hashing Inputs and Verify Hashed Input 
    /// </summary>
    public  class Hasher : IHasher
    {
        #region HashPassword
        /// <summary>
        /// Hash the given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public (byte[] hash, byte[] salt) HashInput(string input)
        {
            try
            {
                var salt = GenerateSalt();
                var hash = GenerateHash(input, salt);

                return (hash, salt);
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region GenerateSalt
        /// <summary>
        /// Generates a random salt of 16 bytes using a secure random number generator.
        /// </summary>
        /// <returns>A byte array containing the generated salt.</returns>
        public byte[] GenerateSalt()
        {
            try
            {
                byte[] salt = new byte[16];

                using var rng = RandomNumberGenerator.Create();

                rng.GetBytes(salt);

                return salt;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region GenerateHash
        /// <summary>
        /// Computes the SHA512 hash of an input combined with a salt using HMACSHA512.
        /// </summary>
        /// <param name="input">The input to hash.</param>
        /// <param name="salt">The salt to combine with the input before hashing.</param>
        /// <returns>A byte array containing the computed hash.</returns>
        public byte[] GenerateHash(string input, byte[] salt)
        {
            try
            {
                using var hmac = new HMACSHA512(salt);

                return hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region VerifyHashedInput
        /// <summary>
        /// Very the Provided Input
        /// </summary>
        /// <param name="providedInput"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool VerifyHashedInput(string providedInput, byte[] salt, byte[] hash)
        {
            try
            {
                var providedPasswordHash = GenerateHash(providedInput, salt);

                var success = providedPasswordHash.SequenceEqual(hash);

                return success;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
