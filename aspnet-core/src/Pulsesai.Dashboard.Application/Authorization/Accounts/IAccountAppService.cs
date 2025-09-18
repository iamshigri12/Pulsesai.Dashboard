using Abp.Application.Services;
using Pulsesai.Dashboard.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
