using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PhoneLelo.Project.Import.MobilePhone.Dto;
using PhoneLelo.Project.Roles.Dto;
using PhoneLelo.Project.Users.Dto;

namespace PhoneLelo.Project.Import.Locations
{
    public interface IRosterLocationAppService : IApplicationService
    {
        Task GetAndSeedPakistanCities();
    }
}
