using System;

namespace Visualisation.Core.Domain
{
	public class VisualisationUser
	{
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string Surname { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }

		public static VisualisationUser GetByUsernameAndPassword(string email, string password)
		{
			throw new NotImplementedException();
		}

		public static VisualisationUser Create(string email, string password)
		{
			throw new NotImplementedException();
		}
	}
}