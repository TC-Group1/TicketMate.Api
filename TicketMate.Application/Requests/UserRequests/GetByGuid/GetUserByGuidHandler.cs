using TicketMate.Persistence.DataRequestObjects.UserRequests;

namespace TicketMate.Application.Requests.UserRequests.GetByGuid
{
    internal class GetUserByGuidHandler : DataRequestResponseHandler<GetUserByGuidRequest, GetUserByGuidResponse>
    {
        public GetUserByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<GetUserByGuidResponse> GetResponseAsync(GetUserByGuidRequest request)
        {
            var userDTO = await _dataAccess.FetchAsync(new GetUserByGuid(request.UserGuid));

            if (userDTO == null)
            {
                throw new DoesNotExistException(nameof(User), (request.UserGuid, nameof(request.UserGuid)));
            }

            return new GetUserByGuidResponse(userDTO.AsDomainUser());
        }
    }
}
