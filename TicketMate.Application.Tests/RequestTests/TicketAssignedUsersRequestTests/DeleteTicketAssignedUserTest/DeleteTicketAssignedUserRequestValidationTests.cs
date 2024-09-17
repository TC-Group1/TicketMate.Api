using TicketMate.Persistence.DataRequestObjects.TicketsAssignedUsersRequest;

namespace TicketMate.Application.Tests.RequestTests.TicketAssignedUsersRequestTests.DeleteTicketAssignedUserTest
{
	public class DeleteTicketAssignedUserRequestValidationTests
	{
		[Fact]
		public void DeleteTicketAssignedUser_Given_TicketGuidNotSet_IsValid_ShouldReturnFalse()
		{
			var request = new DeleteTicketAssignedUser()
			{
				UserGuid = Guid.NewGuid()
			};

			Assert.False(request.IsValid(out _));

		}

		[Fact]
		public void DeleteTicketAssignedUser_Given_TicketGuidIsEmpty_IsValid_ShouldReturnFalse()
		{
			var request = new DeleteTicketAssignedUser()
			{
				UserGuid = Guid.NewGuid(),
				TicketGuid = Guid.Empty,
			};

			Assert.False(request.IsValid(out _));
		}

		[Fact]
		public void DeleteTicketAssignedUser_Given_UserGuidNotSet_IsValid_ShouldReturnFalse()
		{
			var request = new DeleteTicketAssignedUser()
			{
				TicketGuid = Guid.NewGuid(),
			};

			Assert.False(request.IsValid(out _));
		}

		[Fact]
		public void DeleteTicketAssignedUser_Given_UserGuidIsEmpty_IsValid_ShouldReturnFalse()
		{
			var request = new DeleteTicketAssignedUser()
			{
				UserGuid = Guid.Empty,
				TicketGuid = Guid.NewGuid(),
			};

			Assert.False(request.IsValid(out _));
		}

		[Fact]
		public void DeleteTicketAssignedUser_Given_UserGuidIsValidAndTicketGuidIsValid_IsValid_ShouldReturnFalse()
		{
			var request = new DeleteTicketAssignedUser()
			{
				UserGuid = Guid.NewGuid(),
				TicketGuid = Guid.NewGuid(),
			};

			Assert.True(request.IsValid(out _));
		}
	}
}
