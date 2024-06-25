using Moq;
using TicketMate.Application.Requests.UserRequests.UpdateByGuid;
using TicketMate.Domain.Constants;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.UserRequests;

namespace TicketMate.Application.Tests.RequestTests.UserRequestTests.UpdateUserByGuidTests
{
    public class UpdateUserByGuidHandlerTests : HandlerTest
    {
        private readonly UpdateUserByGuidRequest _request = new();

        private readonly UpdateUserByGuidHandler _handler;

        public UpdateUserByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]

        public async Task UpdateUserByGuid_Given_ValidRequest_ShouldReturn_OneRowAffected()
        {
            var rowsAffected = 1;
            _mockDataAccess.Setup(x => x.ExecuteAsync(It.IsAny<UpdateUserByGuid>())).ReturnsAsync(rowsAffected);

            await _handler.ExecuteRequestAsync(_request);

            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public async Task UpdateUserByGuid_Given_InvalidGuid_ShouldThrow_OperationFailedException()
        {
            SetupMockExecuteAsync<UpdateUserByGuid>(0);

            await Assert.ThrowsAsync<OperationFailedException>(async () => await _handler.ExecuteRequestAsync(new UpdateUserByGuidRequest()));
        }

        [Fact]
        public async Task UpdateUserByGuid_Given_DuplicateEntry_ShouldThrow_AlreadyExistsException()
        {

            var request = new UpdateUserByGuidRequest
            {
                PhoneNumber = "980-000-0000"
            };

            var ex = new DataAccessException("Duplicate entry 'testPhoneNumber' for key 'users.PhoneNumber'", MySqlExceptionNumber.DuplicateEntry, null!);

            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<UpdateUserByGuid>())).Throws(ex);

            var result = await Record.ExceptionAsync(async () => await _handler.ExecuteRequestAsync(request));

            Assert.IsType<AlreadyExistsException>(result);
        }
    }
}
