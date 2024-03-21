using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Persistence.DataRequestObjects.UserRequests;

namespace TicketMate.Application.Requests.UserRequests.UpdateByGuid
{
    public class UpdateUserByGuidHandler : DataRequestHandler<UpdateUserByGuidRequest>
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
                if (ex.Message.)
                {

                }

                throw new OperationFailedException();
            }
        }
    }
}
