using AspNet.Identity.MongoDB;
using MongoDB.Driver;
using System;
using Visualisation.Core.Domain;
using Visualisation.Core.Responsitories;

namespace Visualisation.Web
{
	public class ApplicationIdentityContext : IDisposable
	{
		public IMongoCollection<IdentityRole> Roles { get; set; }
		public IMongoCollection<ApplicationUser> Users { get; set; }

		public static ApplicationIdentityContext Create()
		{
			// todo add settings where appropriate to switch server & database in your own application
			var client = new MongoClient("mongodb://localhost:27017");
			var database = client.GetDatabase("mydb");

			var roles = database.GetCollection<IdentityRole>("roles");
			var users = new ApplicationUserRepository().Users;
			return new ApplicationIdentityContext(users, new IdentityRoleRepository().Roles);
		}

		private ApplicationIdentityContext(IMongoCollection<ApplicationUser> users, IMongoCollection<IdentityRole> roles)
		{
			Users = users;
			Roles = roles;
		}

		public void Dispose()
		{
		}
	}

	public class ApplicationUserRepository : MongoDbRepository<ApplicationUser>
	{
		public IMongoCollection<ApplicationUser> Users => Collection;
	}
	public class IdentityRoleRepository : MongoDbRepository<VisualisationIdentityRole>
	{
		public IMongoCollection<VisualisationIdentityRole> Roles => Collection;
	}

	public class VisualisationIdentityRole : IdentityRole, IEntity
	{
		public new Guid Id { get; set; }
	}
}