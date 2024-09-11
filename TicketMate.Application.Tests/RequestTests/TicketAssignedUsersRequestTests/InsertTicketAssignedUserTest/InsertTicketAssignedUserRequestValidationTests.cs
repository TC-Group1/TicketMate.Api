using TicketMate.Application.Requests.TicketAssignedUsersRequests.Insert;

namespace TicketMate.Application.Tests.RequestTests.TicketAssignedUsersRequestTests.InsertTicketAssignedUserTest
{
	public class InsertTicketAssignedUserRequestValidationTests
	{

		#region InvalidGuidTests
		[Fact]
		public void InsertTicketAssignedUserRequest_Given_UserGuidNotProvided_IsValid_ShouldReturnFalse()
		{
			var request = new InsertTicketAssignedUserRequest()
			{
				TicketGuid = Guid.NewGuid(),
			};

			Assert.False(request.IsValid(out _));
		}



		[Fact]
		public void InsertTicketAssignedUserRequest_Given_TicketGuidNotProvided_IsValid_ShouldReturnFalse()
		{
			var request = new InsertTicketAssignedUserRequest()
			{
				UserGuid = Guid.NewGuid(),
			};

			Assert.False(request.IsValid(out _));
		}


		[Fact]
		public void InsertTicketAssignedUserRequest_Given_TicketGuidIsEmpty_IsValid_ShouldReturnFalse()
		{
			var request = new InsertTicketAssignedUserRequest()
			{
				UserGuid = Guid.NewGuid(),
				TicketGuid = Guid.Empty
			};

			Assert.False(request.IsValid(out _));
		}


		[Fact]
		public void InsertTicketAssignedUserRequest_Given_UserGuidIsEmpty_IsValid_ShouldReturnFalse()
		{
			var request = new InsertTicketAssignedUserRequest()
			{
				UserGuid = Guid.Empty,
				TicketGuid = Guid.NewGuid()
			};

			Assert.False(request.IsValid(out _));
		}

		#endregion

		#region ValidRequestTest
		[Fact]
		public void InsertTicketAssignedUserRequest_Given_ValidRequest_IsValid_ShouldReturnTrue()
		{
			var request = new InsertTicketAssignedUserRequest()
			{
				TicketGuid = Guid.NewGuid(),
				UserGuid = Guid.NewGuid()
				
			};

			Assert.True(request.IsValid(out _));
		}

		#endregion
	}
}
