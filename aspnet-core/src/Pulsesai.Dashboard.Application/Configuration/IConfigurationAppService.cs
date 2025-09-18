using Pulsesai.Dashboard.Configuration.Dto;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
