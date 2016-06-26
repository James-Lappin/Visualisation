using MongoDB.Driver;
using Visualisation.Core.Domain;

namespace Visualisation.Core.Responsitories
{
	public class ApplicationUserRepository
	{
		public IMongoCollection<ApplicationUser> Users { get; }

		public ApplicationUserRepository()
		{
			var database = MongoDbHelper.GetDatabase();
			Users = MongoDbHelper.GetCollection<ApplicationUser>(database);
		}

		public void DeleteAll()
		{
			Users.DeleteMany(FilterDefinition<ApplicationUser>.Empty);
		}
	}
}