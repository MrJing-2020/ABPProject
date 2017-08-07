using Abp.Authorization;
using ABPProject.Authorization.Roles;
using ABPProject.MultiTenancy;
using ABPProject.Users;

namespace ABPProject.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
