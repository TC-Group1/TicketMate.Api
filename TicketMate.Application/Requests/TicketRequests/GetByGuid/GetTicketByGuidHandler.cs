using TicketMate.Persistence.DataRequestObjects.TicketRequests;

namespace TicketMate.Application.Requests.TicketRequests.GetByGuid
{
    internal class GetTicketByGuidHandler : DataRequestResponseHandler<GetTicketByGuidRequest, GetTicketByGuidResponse>
    {
        public GetTicketByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<GetTicketByGuidResponse> GetResponseAsync(GetTicketByGuidRequest request)
        {
            var ticketDTO = await _dataAccess.FetchAsync(new GetTicketByGuid(request.TicketGuid));

            if (ticketDTO == null)
            {
                throw new DoesNotExistException(nameof(Ticket), (request.TicketGuid, nameof(request.TicketGuid)));
            }

            return new GetTicketByGuidResponse(ticketDTO.AsDomainTicket());
        }
    }
}
