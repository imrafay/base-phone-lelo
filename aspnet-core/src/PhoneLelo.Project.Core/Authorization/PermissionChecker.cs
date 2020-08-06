using Abp.Authorization;
using PhoneLelo.Project.Authorization.Roles;
using PhoneLelo.Project.Authorization.Users;

namespace PhoneLelo.Project.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
