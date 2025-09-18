using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Pulsesai.Dashboard.EntityFrameworkCore;
using Pulsesai.Dashboard.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Pulsesai.Dashboard.Web.Tests;

[DependsOn(
    typeof(DashboardWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class DashboardWebTestModule : AbpModule
{
    public DashboardWebTestModule(DashboardEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(DashboardWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(DashboardWebMvcModule).Assembly);
    }
}