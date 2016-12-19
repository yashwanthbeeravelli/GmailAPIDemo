using Abp.Authorization;
using GmailAPIDemo.Authorization.Roles;
using GmailAPIDemo.MultiTenancy;
using GmailAPIDemo.Users;

namespace GmailAPIDemo.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
