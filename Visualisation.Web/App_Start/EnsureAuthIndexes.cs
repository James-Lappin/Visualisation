using AspNet.Identity.MongoDB;
using Visualisation.Core.Responsitories;

namespace Visualisation.Web
{
    public class EnsureAuthIndexes
    {
        public static void Exist()
        {
            IndexChecks.EnsureUniqueIndexOnUserName(new ApplicationUserRepository().Users);
            IndexChecks.EnsureUniqueIndexOnRoleName(new IdentityRoleRepository().Roles);
        }
    }
}