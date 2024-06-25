using TicketMate.Domain.Constants;
using TicketMate.Persistence.DataRequestObjects.UserRequests;


namespace TicketMate.Application.Requests.UserRequests.UpdateByGuid
{
    internal class UpdateUserByGuidHandler : DataRequestHandler<UpdateUserByGuidRequest>
    {
        public UpdateUserByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }
        public override async Task ExecuteRequestAsync(UpdateUserByGuidRequest request)
        {
            try
            {
                var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateUserByGuid(
                                                                request.Guid,
                                                                request.FirstName,
                                                                request.LastName,
                                                                request.PhoneNumber,
                                                                request.Email,
                                                                request.Avatar,
                                                                request.IsActive,
                                                                request.PasswordHash));

                if (rowsAffected <= 0)
                {
                    throw new OperationFailedException();
                }
            }
            catch (DataAccessException ex)
            {
                if (ex.ExceptionNumber == (MySqlExceptionNumber.DuplicateEntry))
                {
                    throw new AlreadyExistsException(nameof(User), (request.PhoneNumber, nameof(request.PhoneNumber)), (request.Email, nameof(request.Email)));
                }
                throw new OperationFailedException();
            }
        }
    }
}
