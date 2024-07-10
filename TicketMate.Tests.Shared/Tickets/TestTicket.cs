using System.Diagnostics.CodeAnalysis;
using TicketMate.Persistence.Abstraction;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataTransferObjects;
using TicketMate.Tests.Shared.Helpers;

namespace TicketMate.Tests.Shared.Tickets
{
    /// <summary>
    /// Shared Test Helper Used to Insert and Fetch a Ticket_DTO
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TestTicket
    {
        private static IDataAccess _dataAccess = TestDataAccess.SharedInstance;

        #region InsertTicketAsync
        /// <summary>
        /// Insert a new Test Ticket
        /// </summary>
        /// <param name="ticketGuid">Ticket Guid</param>
        /// <param name="projectGuid">Project Guid</param>
        /// <param name="userGuid">User Guid</param>
        /// <returns></returns>
        public static async Task InsertTicketAsync(Guid ticketGuid, Guid projectGuid, Guid userGuid)
        {
            var insertTicketRequest = new InsertTicket(ticketGuid,
                       projectGuid,
                       TestString.Random(),
                       TestString.Random(),
                       userGuid);

            await _dataAccess.ExecuteAsync(insertTicketRequest);
        }
        #endregion

        #region InsertAndFetchTicketDtoAsync
        /// <summary>
        /// Insert and Fetch a Ticket_DTO
        /// </summary>
        /// <param name="projectGuid">Project Guid</param>
        /// <param name="userGuid">User Guid</param>
        /// <returns></returns>
        public static async Task<Tickets_DTO> InsertAndFetchTicketDtoAsync(Guid projectGuid, Guid userGuid)
        {
            var ticketGuid = Guid.NewGuid();

            var insertTicketRequest = new InsertTicket(ticketGuid,
                       projectGuid,
                       TestString.Random(),
                       TestString.Random(),
                       userGuid);

            await _dataAccess.ExecuteAsync(insertTicketRequest);

            var result = await _dataAccess.FetchAsync(new GetTicketByGuid(ticketGuid));

            return result!;
        }
        #endregion
    }
}
