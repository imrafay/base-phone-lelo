using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PhoneLelo.Project.Import.MobilePhone.Dto;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Roles.Dto;
using PhoneLelo.Project.Store.Dto;
using PhoneLelo.Project.Users.Dto;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public interface IProductStoreAppService : IApplicationService
    {
        Task CreateUserStore(StoreInputDto storeInput);
    }
}
