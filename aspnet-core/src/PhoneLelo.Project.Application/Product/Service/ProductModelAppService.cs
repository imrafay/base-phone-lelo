using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Runtime.Session;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.Product;
using PhoneLelo.Project.Product.Dto;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public class ProductModelAppService : ApplicationService, IProductModelAppService
    {
        private readonly IAbpSession _abpSession; 
        private readonly ProductCompanyManager _productCompanyManager;
        private readonly ProductModelManager _productModelManager;

        public ProductModelAppService(

            IAbpSession abpSession,
            ProductCompanyManager productCompanyManager,
            ProductModelManager productModelManager)
        {
            _abpSession = abpSession;
            _productCompanyManager = productCompanyManager;
            _productModelManager = productModelManager;
        }

        public async Task<List<DropdownOutputDto>> GetProductModelDropdown(long companyId)
        {
            var productModels = await _productModelManager.GetAllByCompanyIdAsync(companyId);
            var productModelDropdown= productModels.Select(x => new DropdownOutputDto()
                {
                    Id =x.Id,
                    Name =x.Model
                }).ToList();

            return productModelDropdown;
        }
    }
}

