using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;

namespace TicketMate.Application.Requests.ProjectRequests.Insert
{
    internal class InsertProjectHandler : DataRequestHandler<InsertProjectRequest>
    {
        public InsertProjectHandler(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public async override Task ExecuteRequestAsync(InsertProjectRequest request)
        {
            try
            {
                var rowsAffected = await _dataAccess.ExecuteAsync(new InsertProject(request.Guid, request.Name));

                if (rowsAffected <= 0)
                {
                    throw new OperationFailedException();
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Message.StartsWith("Duplicate entry"))
                {
                    if (ex.Message.EndsWith("'Projects.Guid_UNIQUE'"))
                    {
                        throw new AlreadyExistsException(nameof(Project), (request.Guid, nameof(request.Guid)));
                    }
                }

                throw new OperationFailedException();
            }
        }
    }
}
