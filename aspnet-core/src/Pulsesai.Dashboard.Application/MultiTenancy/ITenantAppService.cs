using Abp.Application.Services;
using Pulsesai.Dashboard.MultiTenancy.Dto;

namespace Pulsesai.Dashboard.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

