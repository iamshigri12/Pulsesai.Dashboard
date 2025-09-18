using Abp.Authorization;
using Pulsesai.Dashboard.Authorization.Roles;
using Pulsesai.Dashboard.Authorization.Users;

namespace Pulsesai.Dashboard.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
