using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PhoneLelo.Project.MultiTenancy;

namespace PhoneLelo.Project.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
