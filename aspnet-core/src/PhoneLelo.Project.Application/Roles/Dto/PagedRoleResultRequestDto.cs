using Abp.Application.Services.Dto;

namespace PhoneLelo.Project.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

