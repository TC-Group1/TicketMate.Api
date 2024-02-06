using GenFu;
using TicketMate.Application.Requests.UserRequests.DeleteByGuid;
using TicketMate.Domain.Exceptions;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Persistence.DataRequestObjects.UserRolesRequests;
using TicketMate.Persistence.DataTransferObjects;

namespace TicketMate.Application.Tests.RequestTests.UserRequestTests.DeleteUserByGuidTests
{
    public class DeleteUserByGuidHandlerTests : HandlerTest
    {
        private readonly DeleteUserByGuidRequest _request = new();

        private readonly DeleteUserByGuidHandler _handler;

        public DeleteUserByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        #region No User Found Tests

        [Fact]
        public async Task DeleteUserByGuid_Given_UserNotFoundWithGuid_ShouldThrow_DoesNotExistException()
        {
            SetupMockFetchAsync<GetUserByGuid, Users_DTO>(null);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleAsync(_request));
        }

        #endregion

        #region User Found With No UserRoles Tests

        [Fact]
        public async Task DeleteUserByGuid_Given_UserFoundWithGuid_AndNoRoles_ButUserNotDeleted_ShouldThrow_OperationFailedException()
        {
            SetupMockFetchAsync<GetUserByGuid, Users_DTO>(new Users_DTO());

            SetupMockFetchListAsync<GetUserRolesByUserId, UserRoles_DTO>(Enumerable.Empty<UserRoles_DTO>());

            SetupMockExecuteAsync<DeleteUserByGuid>(0);

            await Assert.ThrowsAsync<OperationFailedException>(async () => await _handler.HandleAsync(_request));
        }

        [Fact]
        public async Task DeleteUserByGuid_Given_UserFoundWithGuid_AndNoRoles_AndUserIsDeleted_ShouldReturn_TaskCompletedSuccessfully()
        {
            SetupMockFetchAsync<GetUserByGuid, Users_DTO>(new Users_DTO());

            SetupMockFetchListAsync<GetUserRolesByUserId, UserRoles_DTO>(Enumerable.Empty<UserRoles_DTO>());

            SetupMockExecuteAsync<DeleteUserByGuid>(1);

            var task = _handler.HandleAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }

        #endregion

        #region User Found With UserRoles Tests

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(10, 5)]
        public async Task DeleteUserByGuid_Given_UserFoundWithGuid_AndRolesFound_ButAllRolesNotDeleted_ShouldThrow_OperationFailedException(int numberOfRoles, int numberOfRolesDeleted)
        {
            var userRoles = A.ListOf<UserRoles_DTO>(numberOfRoles);

            SetupMockFetchAsync<GetUserByGuid, Users_DTO>(new Users_DTO());

            SetupMockFetchListAsync<GetUserRolesByUserId, UserRoles_DTO>(userRoles);

            SetupMockExecuteAsync<DeleteUserRolesByUserId>(numberOfRolesDeleted);

            await Assert.ThrowsAsync<OperationFailedException>(async () => await _handler.HandleAsync(_request));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        public async Task DeleteUserByGuid_Given_UserFoundWithGuid_AndRolesFound_AndAllRolesDeleted_ButUserNotDeleted_ShouldThrow_OperationFailedException(int numberOfRoles)
        {
            var userRoles = A.ListOf<UserRoles_DTO>(numberOfRoles);

            SetupMockFetchAsync<GetUserByGuid, Users_DTO>(new Users_DTO());

            SetupMockFetchListAsync<GetUserRolesByUserId, UserRoles_DTO>(userRoles);

            SetupMockExecuteAsync<DeleteUserRolesByUserId>(numberOfRoles);

            SetupMockExecuteAsync<DeleteUserByGuid>(0);

            await Assert.ThrowsAsync<OperationFailedException>(async () => await _handler.HandleAsync(_request));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        public async Task DeleteUserByGuid_Given_UserFoundWithGuid_AndRolesFound_AndAllRolesDeleted_AndUserDeleted_ShouldReturn_TaskCompletedSuccessfully(int numberOfRoles)
        {
            var userRoles = A.ListOf<UserRoles_DTO>(numberOfRoles);

            SetupMockFetchAsync<GetUserByGuid, Users_DTO>(new Users_DTO());

            SetupMockFetchListAsync<GetUserRolesByUserId, UserRoles_DTO>(userRoles);

            SetupMockExecuteAsync<DeleteUserRolesByUserId>(numberOfRoles);

            SetupMockExecuteAsync<DeleteUserByGuid>(1);
            
            var task = _handler.HandleAsync(_request);

            await task;

            Assert.True(task.IsCompletedSuccessfully);
        }

        #endregion
    }
}
