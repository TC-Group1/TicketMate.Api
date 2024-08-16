using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMate.Persistence.DataRequestObjects.ProjectRequests;
using TicketMate.Persistence.DataRequestObjects.ProjectTicketsRequests;
using TicketMate.Persistence.DataRequestObjects.RolesRequests;
using TicketMate.Persistence.DataRequestObjects.TicketRequests;
using TicketMate.Persistence.DataRequestObjects.UserRequests;
using TicketMate.Tests.Shared.Projects;
using TicketMate.Tests.Shared.Tickets;
using TicketMate.Tests.Shared.Users;

namespace TicketMate.Persistence.Tests.DataRequestTests.ProjectTicketTests
{
	public class DeleteProjectTicketTests : BaseDataRequestTest
	{
		[Fact]
		public async Task DeleteProjectTicket_Given_ProjectTicketIsDeleted_ShouldReturn_OneRowAffected()
		{
			var existingUser = await TestUser.InsertAndFetchUsersDtoAsync();
			var existingProject = await TestProject.InsertAndFetchProjectDtoAsync();
			var existingTicket = await TestTicket.InsertAndFetchTicketDtoAsync(existingProject.Guid, existingUser.Guid);

			await _dataAccess.ExecuteAsync(new InsertProjectTicket(existingProject.Id, existingTicket.Id));

			var rowsAffectedWhenDeleting = await _dataAccess.ExecuteAsync(new DeleteProjectTicket(existingProject.Guid, existingTicket.Guid));

			await _dataAccess.ExecuteAsync(new DeleteTicketByGuid(existingTicket.Guid));
			await _dataAccess.ExecuteAsync(new DeleteProjectByGuid(existingProject.Guid));
			await _dataAccess.ExecuteAsync(new DeleteUserByGuid(existingUser.Guid));

			Assert.Equal(1, rowsAffectedWhenDeleting);
		}

		[Fact]
		public async Task DeleteProjectTicket_Given_ProjectTicketIsNotDeleted_ShouldReturn_NoRowsAffected()
		{
			var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteProjectTicket(Guid.NewGuid(), Guid.NewGuid()));

			Assert.Equal(0, rowsAffected);
		}
	}
}
