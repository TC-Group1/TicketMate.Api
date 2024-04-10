namespace TicketMate.Persistence.DataRequestObjects.UserRolesRequests
{
    public class GetUserRolesByUserId : IDataFetch<UserRoles_DTO>
    {
        public GetUserRolesByUserId(int userId) => UserId = userId;

        public int UserId { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => $@"SELECT UserRoles.Id AS `UserRoles Table ID`, 
                                  UserId, 
                                  RoleId, 
                                  Roles.Id AS `Roles Table ID`, 
                                  Roles.Name AS `Name of Role`, 
                                  Concat(Users.FirstName, ' ', Users.LastName) AS `User Full Name`
                                  FROM {DatabaseTable.UserRoles}
                                  JOIN Roles ON UserRoles.RoleId = Roles.Id
                                  JOIN Users On UserRoles.UserId = Users.Id
                                  WHERE UserId = @userId;";
    }
}
