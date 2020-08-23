using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PhoneLelo.Project.Import.MobilePhone.Dto;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Roles.Dto;
using PhoneLelo.Project.Users.Dto;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public interface IProductAdvertAppService : IApplicationService
    {
        Task Create(ProductAdvertInputDto input);
        Task Update(ProductAdvertInputDto input);

        Task<ListResultDto<ProductAdvertViewDto>> GetAll(
            ProductAdvertFilterInputDto filter);
    }
}
