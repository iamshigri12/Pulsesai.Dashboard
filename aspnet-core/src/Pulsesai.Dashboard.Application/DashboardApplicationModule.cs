using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using Pulsesai.Dashboard.Authorization;
using Pulsesai.Dashboard.Telemetry;

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
    public override void PostInitialize()
    {
        
        var workerManager = IocManager.Resolve<IBackgroundWorkerManager>();
        workerManager.Add(IocManager.Resolve<TelemetryGeneratorWorker>());
        workerManager.Add(IocManager.Resolve<TelemetryPurgeWorker>());
 
    }
}
