using TicketMate.Application.Requests.ProjectRequests.GetByGuid;

namespace TicketMate.Application.Requests.TicketRequests.GetByGuid
{
    internal class GetTicketByGuidHandler : DataRequestResponseHandler<GetTicketByGuidRequest, GetTicketByGuidResponse>
    {
        public GetTicketByGuidHandler(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public override Task<GetTicketByGuidResponse> GetResponseAsync(GetTicketByGuidRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
