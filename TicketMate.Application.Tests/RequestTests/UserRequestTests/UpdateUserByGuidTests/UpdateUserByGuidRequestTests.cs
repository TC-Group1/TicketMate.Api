using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Application.Requests.UserRequests.UpdateByGuid;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.Tests.DataRequestTests.Helpers;

namespace TicketMate.Application.Tests.RequestTests.UserRequestTests.UpdateUserByGuidTests
{
    public class UpdateUserByGuidRequestTests
    {
        [Fact]
        public void UpdateUserByGuidRequest_Given_ValidData_IsValid_ShouldReturn_True()
        {
            var guid = Guid.NewGuid();

            
            var request = new UpdateUserByGuidRequest(guid,
                                        TestString.Random(),
                                        TestString.Random(),
                                        TestString.Random(14),
                                        TestString.Random(),
                                        TestString.Random(),
                                        1,
                                        TestString.Random());
            var isValid = request.IsValid(out _);

            Assert.True(isValid, "Request should be considered valid.");
            
        }
    }
}
