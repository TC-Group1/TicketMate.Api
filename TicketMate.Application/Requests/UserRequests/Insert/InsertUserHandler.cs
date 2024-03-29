﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Persistence.DataRequestObjects.UserRequests;

namespace TicketMate.Application.Requests.UserRequests.Insert
{
    internal class InsertUserHandler : DataRequestHandler<InsertUserRequest>
    {
        public InsertUserHandler(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public async override Task ExecuteRequestAsync(InsertUserRequest request)
        {
            try
            {
                var rowsAffected = await _dataAccess.ExecuteAsync(new InsertUser(
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
                if (ex.Message.StartsWith("Duplicate entry"))
                {
                    if (ex.Message.EndsWith("'users.Guid'"))
                    {
                        throw new AlreadyExistsException(nameof(User), (request.Guid, nameof(request.Guid)));
                    }

                    if (ex.Message.EndsWith("'users.unique_username'"))
                    {
                        var nameTuple = (request.FirstName, request.LastName);

                        throw new AlreadyExistsException(nameof(User), nameTuple);
                    }
                }

                throw new OperationFailedException();
                   
            }
        }
    }
}
