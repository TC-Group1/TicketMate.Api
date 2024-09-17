namespace TicketMate.Persistence.DataRequestObjects.ProjectUserRequests;

public class DeleteProjectUser : IDataExecute
{
    public DeleteProjectUser(Guid projectGuid, Guid userGuid)
    {
        ProjectGuid = projectGuid;
        UserGuid = userGuid;
    }
    public Guid ProjectGuid { get; set; }
    
    public Guid UserGuid { get; set; }
    
    public object? GetParameters() => this;
    
    public string GetSql() => 
        $@"
            DELETE FROM {DatabaseTable.ProjectUsers} WHERE ProjectGuid = @ProjectGuid AND UserGuid = @UserGuid; 
           
         ";
}