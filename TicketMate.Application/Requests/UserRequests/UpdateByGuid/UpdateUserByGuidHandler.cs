using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            catch (MySqlException ex)
            {
                if (ex.Number == (MySqlExceptionNumber.DuplicateEntry))
                {
                    if (ex.Message.EndsWith("'users.PhoneNumber'"))
                    {
                        throw new AlreadyExistsException(nameof(User), (request.PhoneNumber, nameof(request.PhoneNumber)));
                    }


                    if (ex.Number == (MySqlExceptionNumber.DuplicateEntry))
                    {
                        if (ex.Message.EndsWith("'users.Email'"))
                        {
                            throw new AlreadyExistsException(nameof(User), (request.Email, nameof(request.Email)));
                        }
                    }
                }

                throw new OperationFailedException();
            }
        }
    }
}
