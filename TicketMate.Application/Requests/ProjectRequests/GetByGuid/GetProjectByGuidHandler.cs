using TicketMate.Persistence.DataRequestObjects.ProjectRequests;

namespace TicketMate.Application.Requests.ProjectRequests.GetByGuid
{
    internal class GetProjectByGuidHandler : DataRequestResponseHandler<GetProjectByGuidRequest, GetProjectByGuidResponse>
    {
        public GetProjectByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<GetProjectByGuidResponse> GetResponseAsync(GetProjectByGuidRequest request)
        {
            var projectDTO = await _dataAccess.FetchAsync(new GetProjectByGuid(request.ProjectGuid));

            if (projectDTO == null)
            {
                throw new DoesNotExistException(nameof(Project), (request.ProjectGuid, nameof(request.ProjectGuid)));
            }

            return new GetProjectByGuidResponse(projectDTO.AsDomainProject());
        }
    }
}
