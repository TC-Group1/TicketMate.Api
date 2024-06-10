namespace TicketMate.Application.Requests.TicketRequests.Insert
{
    internal class InsertTicketHandler : DataRequestHandler<InsertTicketRequest>
    {
        public InsertTicketHandler(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public override Task ExecuteRequestAsync(InsertTicketRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
