using Abp.Authorization;
using Abp.Runtime.Session;
using Pulsesai.Dashboard.Configuration.Dto;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : DashboardAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
