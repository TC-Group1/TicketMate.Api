using System.Collections.Generic;

namespace TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest
{
    public class GetAssignedUsersByTicketId : IDataFetch<Users_DTO>
    {

        public GetAssignedUsersByTicketId( int ticketId)
        {
            TicketId = ticketId;
            
        }

        public int TicketId { get; set; }

        public string GetSql() => $@"SELECT {DatabaseTable.Users}.* 
                                    FROM {DatabaseTable.Users} 
                                    LEFT JOIN {DatabaseTable.TicketsAssignedUsers} 
                                        ON {DatabaseTable.Users}.Id = {DatabaseTable.TicketsAssignedUsers}.UserId 
                                    WHERE {DatabaseTable.TicketsAssignedUsers}.TicketId = @TicketId;";

        public object? GetParameters() => this;
    }
}
