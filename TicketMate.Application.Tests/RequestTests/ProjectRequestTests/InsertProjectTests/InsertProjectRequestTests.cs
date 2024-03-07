using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Application.Requests.ProjectRequests.Insert;
using TicketMate.Domain.Constants;

namespace TicketMate.Application.Tests.RequestTests.ProjectRequestTests.InsertProjectTests
{
    public class InsertProjectRequestTests
    {
        #region InvalidGuidTests

        [Fact]
        public void InsertProjectRequest_Given_ProjectGuidNotProvided_IsValid_ShouldReturnFalse()
        {
            var request = new InsertProjectRequest()
            {
                Name = "Name"
            };

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void InsertProjectRequest_Given_ProjectGuidIsEmpty_IsValid_ShouldReturnFalse()
        {
            var request = new InsertProjectRequest()
            {
                Guid = Guid.Empty,
                Name = "Name"
            };

            Assert.False(request.IsValid(out _));
        }

        #endregion

        #region InvalidNameTests

        [Fact]
        public void InsertProjectRequest_Given_NameIsTooLong_IsValid_ShouldReturnFalse()
        {
            var request = new InsertProjectRequest()
            {
                Guid = Guid.NewGuid(),
                Name = new string('j', MaxLength.ProjectName + 1)
            };

            Assert.False(request.IsValid(out _));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData(null)]
        public void InsertProjectRequest_Given_NameIsNullEmptyOrWhitespace_IsValid_ShouldReturnFalse(string invalidName)
        {
            var request = new InsertProjectRequest(Guid.NewGuid(), invalidName);

            Assert.False(request.IsValid(out _));
        }

        #endregion

        #region ValidRequestTest
        [Fact]
        public void InsertProjectRequest_Given_ValidRequest_IsValid_ShouldReturnTrue()
        {
            var request = new InsertProjectRequest( Guid.NewGuid(), "Mack" );

            Assert.True(request.IsValid(out _));
        }
        #endregion

    }
}
