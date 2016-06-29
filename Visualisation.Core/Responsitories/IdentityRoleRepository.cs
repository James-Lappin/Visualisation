using AspNet.Identity.MongoDB;
using MongoDB.Driver;

namespace Visualisation.Core.Responsitories
{
    public class IdentityRoleRepository
    {
        public IMongoCollection<IdentityRole> Roles { get; private set; }

        public IdentityRoleRepository()
        {
            Roles = MongoDbHelper.GetCollection<IdentityRole>();
        }
    }
}