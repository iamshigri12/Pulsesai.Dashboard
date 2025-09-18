using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Pulsesai.Dashboard.Controllers
{
    public abstract class DashboardControllerBase : AbpController
    {
        protected DashboardControllerBase()
        {
            LocalizationSourceName = DashboardConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
