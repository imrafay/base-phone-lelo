using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.FileManagement;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Roles.Dto;
using PhoneLelo.Project.Storage.FileManagement;
using PhoneLelo.Project.Utils;

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

        [AbpAllowAnonymous]
        public async Task<ListResultDto<ProductAdvertViewDto>> GetAll(
            ProductAdvertFilterInputDto filter)
        {
            var query = _productAdvertManager.GetAllQuery(filter);
            var totalCount = query.Count();

            if (query.Any() && filter.pagedAndSort != null)
            {
                var pagedAndSort = filter.pagedAndSort;
                var sortBy = pagedAndSort.SortBy.GetDescription();
                query = query.OrderBy(sortBy).PageResult(
                    pagedAndSort.Page,
                    pagedAndSort.PageSize).Queryable;
            }
            var output = query.ToList();

            PopulateAdvertImage(output);

            return new PagedResultDto<ProductAdvertViewDto>(
                totalCount: totalCount,
                items: output);
        }

        [AbpAllowAnonymous]
        public async Task<ProductAdvertDetailViewDto> GetProductAdverForEdit(long id)
        {
            try
            {
                var productAdvert = await _productAdvertManager
                     .GetByIdAsync(id);

                if (productAdvert == null)
                {
                    return null;
                }

                var productAdvertBatteryUsages = await _productAdvertManager
                    .GetProducAdverBatteryUsagesById(id);

                var images = await _productAdvertManager
                    .GetProducAdverImagesById(id);

                var productAdvertOutput = ObjectMapper
                    .Map<ProductAdvertDto>(productAdvert);

                var productAdvertBatteryUsageList = (ObjectMapper.Map<List<ProductAdvertBatteryUsageDto>>
                    (productAdvertBatteryUsages));

                var productAdvertImageList = (ObjectMapper.Map<List<ProductAdvertImageDto>>
                    (images));

                return new ProductAdvertDetailViewDto
                {
                    ProductCompanyName=productAdvert.ProductModelFk.Brand,
                    ProductModelName=productAdvert.ProductModelFk.Model,
                    Views = GetProductAdverViews(id),
                    ProductAdvert = productAdvertOutput,
                    ProductAdvertBatteryUsages = productAdvertBatteryUsageList,
                    Images = productAdvertImageList
                };
            }
            catch (Exception ex)
            {
                Logger.Error($"GetProductAdverForEdit > ERROR > {ex.Message}");
                throw new UserFriendlyException($"GetProductAdverForEdit > ERROR > {ex.Message}");
            }
        }

        private int GetProductAdverViews(long id)
        {
            return 0;
        }

        private void PopulateAdvertImage(
            List<ProductAdvertViewDto> productAdverts)
        {
            foreach (var productAdvert in productAdverts)
            {
                //TODO : Resolve _fileStorageManager
                //productAdvert.PrimaryProductImage = _fileStorageManager
                //    .GenerateBlobUrl(
                //    productAdvert.PrimaryProductImage,
                //    PhoneLeloDataFileType.ProductImages
                //    );
            }
        }
    }
}

