using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Application.Requests.ProjectRequests.GetByGuid
{
    public class GetProjectByGuidResponse
    {
        public GetProjectByGuidResponse(Project project) => Project = project;

        public Project Project { get; set; }
    }
}
