using Abp.Application.Services;
using PhoneLelo.Project.MultiTenancy.Dto;

namespace PhoneLelo.Project.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

