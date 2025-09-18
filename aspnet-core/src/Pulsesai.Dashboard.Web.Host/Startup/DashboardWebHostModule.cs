using Abp.Modules;
using Abp.Reflection.Extensions;
using Pulsesai.Dashboard.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Pulsesai.Dashboard.Web.Host.Startup
{
    [DependsOn(
       typeof(DashboardWebCoreModule))]
    public class DashboardWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DashboardWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DashboardWebHostModule).GetAssembly());
        }
    }
}
