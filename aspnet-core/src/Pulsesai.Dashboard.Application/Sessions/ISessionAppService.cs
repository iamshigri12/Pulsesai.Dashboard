using Abp.Application.Services;
using Pulsesai.Dashboard.Sessions.Dto;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
