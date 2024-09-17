using TicketMate.Domain.Models;

namespace TicketMate.Persistence.DataRequestObjects.UserRequests;

public class GetUsersByProjectGuid : IDataFetch<Users_DTO>
{
    public GetUsersByProjectGuid(Guid projectGuid)
    {
        ProjectGuid = projectGuid;
    }

    public Guid ProjectGuid { get; set; }
    public object? GetParameters() => this;

    public string GetSql() =>
        $@"
            SELECT {DatabaseTable.Users}.* FROM {DatabaseTable.Users} 
            INNER JOIN {DatabaseTable.ProjectUsers} 
                ON {DatabaseTable.Users}.Id = {DatabaseTable.ProjectUsers}.UserId
            INNER JOIN {DatabaseTable.Projects}
                ON {DatabaseTable.ProjectUsers}.ProjectId = {DatabaseTable.Projects}.Id
            WHERE {DatabaseTable.Projects}.Guid = @ProjectGuid;
        ";
}