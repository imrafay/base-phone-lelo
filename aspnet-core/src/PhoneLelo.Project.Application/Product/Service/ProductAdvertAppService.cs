using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Extensions;
using Abp.Runtime.Session;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.Product.Dto;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public class ProductAdvertAppService : ApplicationService, IProductAdvertAppService
    {
        private readonly IAbpSession _abpSession;
        private readonly ProductAdvertManager _productAdvertManager;

        public ProductAdvertAppService(

            IAbpSession abpSession,
            ProductAdvertManager productAdvertManager)
        {
            _abpSession = abpSession;
            _productAdvertManager = productAdvertManager;
        }

        public async Task Create(ProductAdvertInputDto input)
        {
            if (input == null)
            {
                return;
            }

            #region Add Product Advert
            var productAdvert = ObjectMapper
                   .Map<ProductAdvert>(input.ProductAdvertinput);

            if (productAdvert == null)
            {
                return;
            }

            await _productAdvertManager.CreateAsync(
                productAdvert: productAdvert);
            #endregion

            #region Add Product Advert Battery Usages
            var productAdvertBatteryUsages = input.ProductAdvertBatteryUsages
                  .Where(x => x.Hours > 0)
                  .Select(x => new ProductAdvertBatteryUsage()
                  {
                      Hours = x.Hours,
                      BatteryUsageType = x.BatteryUsageType,
                      ProductAdvertId = productAdvert.Id
                  }).ToList();

            if (productAdvertBatteryUsages.Any())
            {
                await _productAdvertManager.CreateBatteryUsageAsync(
                    list: productAdvertBatteryUsages);
            }
            #endregion

            #region Add Product Advert Images
            var productAdvertImages = input.Images
                    .Where(x => !x.Image.IsNullOrEmpty())
                   .Select(x => new ProductAdvertImage()
                   {
                       Image = x.Image,
                       ImagePriority = x.ProductImagePriority,
                       ProductAdvertId = productAdvert.Id
                   }).ToList();

            if (productAdvertImages.Any())
            {
                await _productAdvertManager.CreateProductAdvertImageAsync(
                    list: productAdvertImages);
            }
            #endregion
        }

        public async Task Update(ProductAdvertInputDto input)
        {
            var productAdvert = await _productAdvertManager.GetByIdAsync(
                id: input.ProductAdvertinput.Id);

            if (productAdvert == null)
            {
                return;
            }

            #region Update Product Advert

            ObjectMapper.Map(input.ProductAdvertinput, productAdvert);

            await _productAdvertManager.UpdateAsync(
                productAdvert: productAdvert);
            #endregion

            #region Update Product Advert Battery Usages
            var productAdvertBatteryUsages = input.ProductAdvertBatteryUsages
                  .Where(x => x.Hours > 0)
                  .Select(x => new ProductAdvertBatteryUsage()
                  {
                      Hours = x.Hours,
                      BatteryUsageType = x.BatteryUsageType,
                      ProductAdvertId = productAdvert.Id
                  }).ToList();

            if (productAdvertBatteryUsages.Any())
            {
                await _productAdvertManager.UpdateBatteryUsageAsync(
                    list: productAdvertBatteryUsages,
                    productAdvertId: productAdvert.Id);
            }
            #endregion

            #region Add Product Advert Images
            var productAdvertImages = input.Images
                    .Where(x => !x.Image.IsNullOrEmpty())
                   .Select(x => new ProductAdvertImage()
                   {
                       Image = x.Image,
                       ImagePriority = x.ProductImagePriority,
                       ProductAdvertId = productAdvert.Id
                   }).ToList();

            if (productAdvertImages.Any())
            {
                await _productAdvertManager.CreateProductAdvertImageAsync(
                    list: productAdvertImages);
            }
            #endregion
        }

        public async Task<List<ProductAdvertDetailViewDto>> GetAll(
            ProductAdvertFilterInputDto filter)
        {
            var query = _productAdvertManager.GetAll(filter);

            return null;
        }
    }
}

