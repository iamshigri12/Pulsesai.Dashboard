using Abp.MultiTenancy;
using Pulsesai.Dashboard.Authorization.Users;

namespace Pulsesai.Dashboard.MultiTenancy;

public class Tenant : AbpTenant<User>
{
    public Tenant()
    {
    }

    public Tenant(string tenancyName, string name)
        : base(tenancyName, name)
    {
    }
}
