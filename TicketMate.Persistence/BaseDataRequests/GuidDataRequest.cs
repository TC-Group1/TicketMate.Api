namespace TicketMate.Persistence.BaseDataRequests
{
    /// <summary>
    /// This base request can be reused by any IDataRequest which will GetParameters() => { Guid };
    /// </summary>
    public abstract class GuidDataRequest : IDataRequest
    {
        public Guid Guid { get; set; }

        public GuidDataRequest(Guid guid) => Guid = guid;

        public object? GetParameters() => new { Guid };

        public abstract string GetSql();
    }
}
