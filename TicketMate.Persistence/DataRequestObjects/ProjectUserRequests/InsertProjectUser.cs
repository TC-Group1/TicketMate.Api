namespace TicketMate.Persistence.DataRequestObjects.ProjectUserRequests;

public class InsertProjectUser : IDataExecute
{
    public InsertProjectUser(Guid projectGuid, Guid userGuid)
    {
        ProjectGuid = projectGuid;
        UserGuid = userGuid;
    }

    public Guid ProjectGuid { get; set; }

    public Guid UserGuid { get; set; }

    public object? GetParameters() => this;

    public string GetSql() =>
        $@"
            INSERT INTO {DatabaseTable.ProjectUsers} (ProjectId, UserId) 
            VALUES (
                    (SELECT Id FROM {DatabaseTable.Projects} WHERE Guid = @ProjectGuid),
                    (SELECT Id FROM {DatabaseTable.Users} WHERE Guid = @UserGuid)
            )
         ";
}