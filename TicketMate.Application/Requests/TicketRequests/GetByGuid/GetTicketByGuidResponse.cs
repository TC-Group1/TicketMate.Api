namespace TicketMate.Application.Requests.TicketRequests.GetByGuid
{
    public class GetTicketByGuidResponse
    {
        public GetTicketByGuidResponse(Ticket ticket) => Ticket = ticket;

        public Ticket Ticket { get; set; }
    }
}
