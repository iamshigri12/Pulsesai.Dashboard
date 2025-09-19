using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Pulsesai.Dashboard.Authorization;

namespace Pulsesai.Dashboard;

[DependsOn(
    typeof(DashboardCoreModule),
    typeof(AbpAutoMapperModule))]
public class DashboardApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<DashboardAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(DashboardApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
        Configuration.BackgroundJobs.IsJobExecutionEnabled = true; 

    }
}
