using TicketMate.Domain.Extensions;

namespace TicketMate.Domain.Tests.ExtensionTests
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("AbCdEf ", "abcdef")]
        [InlineData(" Admin ", "admin")]
        [InlineData("TestUser ", "testuser")]
        [InlineData(" Simple Case ", "simple case")]
        [InlineData("MIXEDcaseInput", "mixedcaseinput")]
        [InlineData(" Spaces Before And After ", "spaces before and after")]
        [InlineData("1234 with Text", "1234 with text")]
        [InlineData("Special!@#Characters", "special!@#characters")]
        [InlineData("CamelCaseToLowerCase", "camelcasetolowercase")]
        [InlineData("alreadylowercase", "alreadylowercase")]
        public void LowerAndTrim_ConvertsToLowercaseAndTrimsWhitespace_Success(string actual, string expected)
        {
            Assert.NotNull(actual.LowerAndTrim());
            Assert.Equal(actual.LowerAndTrim(), expected);
        }

        [Theory]
        [InlineData(null, null)]
        public void LowerAndTrim_StringIsNull_Returns_Null_Success(string actual, string expected)
        {
            Assert.Null(actual.LowerAndTrim());
            Assert.Equal(actual.LowerAndTrim(), expected);
        }

        [Theory]
        [InlineData("", "")]
        public void LowerAndTrim_StringIsEmpty_Returns_EmptyString_Success(string actual, string expected)
        {
            Assert.NotNull(actual.LowerAndTrim());
            Assert.Equal(actual.LowerAndTrim(), expected);
        }
    }
}
