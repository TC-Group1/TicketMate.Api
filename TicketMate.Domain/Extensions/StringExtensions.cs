namespace TicketMate.Domain.Extensions
{
    /// <summary>
    /// String Extension Methods
    /// </summary>
    public static class StringExtensions
    {
        #region LowerAndTrim
        /// <summary>
        /// Extension Method used to Lower and Trim a String Value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string LowerAndTrim(this string input)
        {
            if (!string.IsNullOrEmpty(input))
                return input.ToLower().Trim();

            return input;
        }
        #endregion
    }
}
