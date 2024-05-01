namespace TicketMate.Application.Requests.ProjectRequests.GetByGuid
{
    public class GetProjectByGuidResponse
    {
        public GetProjectByGuidResponse(Project project) => Project = project;

        public Project Project { get; set; }
    }
}
