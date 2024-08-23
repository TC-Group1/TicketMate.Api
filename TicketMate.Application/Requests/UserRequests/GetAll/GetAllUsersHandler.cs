using TicketMate.Persistence.DataRequestObjects.UserRequests;

namespace TicketMate.Application.Requests.UserRequests.GetAll
{
    internal class GetAllUsersHandler : DataRequestResponseHandler<GetAllUsersRequest, IEnumerable<User>>
    {
        public GetAllUsersHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<IEnumerable<User>> GetResponseAsync(GetAllUsersRequest request)
        {
            var users = await _dataAccess.FetchListAsync(new GetAllUsers());

            if (users.Any())
            {
                return users.Select(_ => _.AsDomainUser());
            }

            return Enumerable.Empty<User>();
        }
    }
}
