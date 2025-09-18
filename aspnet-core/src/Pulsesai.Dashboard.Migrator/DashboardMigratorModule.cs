using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Pulsesai.Dashboard.Configuration;
using Pulsesai.Dashboard.EntityFrameworkCore;
using Pulsesai.Dashboard.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace Pulsesai.Dashboard.Migrator;

[DependsOn(typeof(DashboardEntityFrameworkModule))]
public class DashboardMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public DashboardMigratorModule(DashboardEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(DashboardMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            DashboardConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(DashboardMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
