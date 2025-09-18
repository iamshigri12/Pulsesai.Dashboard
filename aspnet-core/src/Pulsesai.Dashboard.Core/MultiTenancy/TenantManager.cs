using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Pulsesai.Dashboard.Authorization.Users;
using Pulsesai.Dashboard.Editions;

namespace Pulsesai.Dashboard.MultiTenancy;

public class TenantManager : AbpTenantManager<Tenant, User>
{
    public TenantManager(
        IRepository<Tenant> tenantRepository,
        IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
        EditionManager editionManager,
        IAbpZeroFeatureValueStore featureValueStore)
        : base(
            tenantRepository,
            tenantFeatureRepository,
            editionManager,
            featureValueStore)
    {
    }
}
