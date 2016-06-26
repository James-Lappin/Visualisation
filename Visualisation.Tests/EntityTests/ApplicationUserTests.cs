using Microsoft.AspNet.Identity.Owin;
using NUnit.Framework;
using System.Threading.Tasks;
using Visualisation.Core.Domain;
using Visualisation.Core.Responsitories;
using Visualisation.Web;

namespace Visualisation.Tests.EntityTests
{
	public class ApplicationUserTests
	{
		private const string Username = "username";
		private const string Email = "this@email.com";
		private const string Password = "Password1!";

		private ApplicationUserManager UserManager { get; set; }

		[SetUp]
		public void SetUp()
		{
			UserManager = ApplicationUserManager.Create(new IdentityFactoryOptions<ApplicationUserManager>());
		}

		[OneTimeTearDown]
		public void TestTearDown()
		{
			new ApplicationUserRepository().DeleteAll();
		}

		[Test]
		public async Task CreateApplicationUser()
		{
			//arrange
			var user = new ApplicationUser { UserName = Username, Email = Email };

			//act
			var result = await UserManager.CreateAsync(user, Password);

			//assert
			Assert.That(result.Succeeded, string.Join(" ,", result.Errors));
		}
	}
}
