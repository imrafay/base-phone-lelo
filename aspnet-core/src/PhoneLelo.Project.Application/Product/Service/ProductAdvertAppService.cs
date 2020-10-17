using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.UI;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.FileManagement;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Product.Enum;
using PhoneLelo.Project.Roles.Dto;
using PhoneLelo.Project.Storage.FileManagement;
using PhoneLelo.Project.Utils;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public class ProductAdvertAppService : ApplicationService, IProductAdvertAppService
    {
        private readonly IAbpSession _abpSession;
        private readonly ProductAdvertManager _productAdvertManager;
        private readonly ProductAdvertViewLogManager _productAdvertViewLogManager;
        private readonly ProductModelManager _productModelManager;
        private readonly IFileStorageManager _fileStorageManager;

        public ProductAdvertAppService(

            IAbpSession abpSession,
            ProductAdvertManager productAdvertManager,
            ProductAdvertViewLogManager productAdvertViewLogManager,
            IFileStorageManager fileStorageManager, ProductModelManager productModelManager)
        {
            _abpSession = abpSession;
            _productAdvertManager = productAdvertManager;
            _productAdvertViewLogManager = productAdvertViewLogManager;
            _fileStorageManager = fileStorageManager;
            _productModelManager = productModelManager;
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

            productAdvert.UserId = AbpSession.UserId;
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

            #region Add Product Advert Accessory

            var productAdvertAccessories = input.ProductAdvertAccessories
                       .Select(x => new ProductAdvertAccessory()
                       {
                           ProductAdvertId = productAdvert.Id,
                           AccessoryType = x.AccessoryType
                       }).ToList();

            if (productAdvertAccessories.Any())
            {
                await _productAdvertManager.CreateProductAdvertAccessoryAsync(
                    list: productAdvertAccessories);
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
            productAdvert.UserId = AbpSession.UserId;
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

            #region Update Product Advert Accessory
            var productAdvertAccessories = input.ProductAdvertAccessories
                  .Select(x => new ProductAdvertAccessory()
                  {
                      AccessoryType = x.AccessoryType,
                      ProductAdvertId = productAdvert.Id
                  }).ToList();

            if (productAdvertAccessories.Any())
            {
                await _productAdvertManager.UpdateProductAccessoryAsync(
                    list: productAdvertAccessories,
                    productAdvertId: productAdvert.Id);
            }
            #endregion
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<ProductAdvertViewDto>> GetAll(
            ProductAdvertFilterInputDto filter)
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant))
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
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
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<ProductAdvertViewDto>> GetRelatedAdsByAdvertId(
            long productAdvertId,
            int page,
            int pageSize)
        {
            var productAdvert = await _productAdvertManager.GetByIdAsync(productAdvertId);
            if (productAdvert == null)
            {
                throw new UserFriendlyException("Ad not found.");
            }

            var filter = new ProductAdvertFilterInputDto()
            {
                ProductModelId = productAdvert.ProductModelId,
                CityId = productAdvert.CityId,
                MaxPrice = productAdvert.Price + AppConsts.RelatedProductAdPriceLimit,
                MinPrice = productAdvert.Price - AppConsts.RelatedProductAdPriceLimit,
                pagedAndSort = new PagedAndSortDto()
                {
                    Page = page,
                    PageSize = pageSize,
                    SortBy = SortByEnum.Newest
                }
            };

            var result = await GetAll(filter);
            result.Items = result.Items
                .Where(x => x.Id != productAdvertId)
                .ToList();

            if (result.Items.Any() == false)
            {
                var getAllfilter = new ProductAdvertFilterInputDto()
                {
                    pagedAndSort = new PagedAndSortDto()
                    {
                        Page = page,
                        PageSize = pageSize,
                        SortBy = SortByEnum.Newest
                    }
                };

               return await GetAll(getAllfilter);
            }

            return result;
        }

        [AbpAllowAnonymous]
        public async Task<ProductAdvertDetailViewDto> GetProductAdvertForEdit(long id)
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

                var productAdvertAccessories = await _productAdvertManager
                    .GetProductAdvertAccessoriesById(id);

                var productAdvertOutput = ObjectMapper
                    .Map<ProductAdvertDto>(productAdvert);

                var productAdvertBatteryUsageList = (ObjectMapper.Map<List<ProductAdvertBatteryUsageDto>>
                    (productAdvertBatteryUsages));

                var productAdvertImageList = (ObjectMapper.Map<List<ProductAdvertImageDto>>
                    (images));

                var productAdvertAccessoryList = (ObjectMapper.Map<List<ProductAdvertAccessoryDto>>
                    (productAdvertAccessories));

                var viewsCount = await GetProductAdverViews(id);
                return new ProductAdvertDetailViewDto
                {
                    ProductModelId = productAdvert.ProductModelId,
                    ProductCompanyName = productAdvert.ProductModelFk.Brand,
                    ProductModelName = productAdvert.ProductModelFk.Model,
                    Views = viewsCount,
                    ProductAdvert = productAdvertOutput,
                    ProductAdvertBatteryUsages = productAdvertBatteryUsageList,
                    Images = productAdvertImageList,
                    productAdvertAccessories = productAdvertAccessoryList
                };
            }
            catch (Exception ex)
            {
                Logger.Error($"GetProductAdverForEdit > ERROR > {ex.Message}");
                throw new UserFriendlyException($"GetProductAdverForEdit > ERROR > {ex.Message}");
            }
        }

        [AbpAllowAnonymous]
        public async Task<ProductAdvertDetailViewDto> GetProductAdvertForDetailView(
            long advertId)
        {
            await _productAdvertViewLogManager.CreateAsync(
                advertId: advertId,
                userId: AbpSession.UserId);

            var detail = await GetProductAdvertForEdit(advertId);

            detail.ProductModelSpecification = await _productModelManager.GetProductModelById(detail.ProductModelId);
            return detail;
        }

        [AbpAllowAnonymous]
        public List<DropdownOutputDto> GetRamDropDown()
        {
            var ramEnums = Enum.GetValues(typeof(RamEnum)).Cast<RamEnum>();

            return ramEnums
                     .Select(
                         x => new DropdownOutputDto()
                         {
                             Id = (long)x,
                             Name = x.GetDescription()
                         }).ToList();
        }

        [AbpAllowAnonymous]
        public List<DropdownOutputDto> GetStorageDropDown()
        {
            var storageEnums = Enum.GetValues(typeof(StorageEnum)).Cast<StorageEnum>();

            return storageEnums
                     .Select(
                         x => new DropdownOutputDto()
                         {
                             Id = (long)x,
                             Name = x.GetDescription()
                         }).ToList();
        }

        [AbpAllowAnonymous]
        public List<DropdownOutputDto> GetAccessoriesDropDown()
        {
            var accessoryEnums = Enum.GetValues(typeof(ProductAccessoryEnum))
                .Cast<ProductAccessoryEnum>();

            return accessoryEnums
                     .Select(
                         x => new DropdownOutputDto()
                         {
                             Id = (long)x,
                             Name = x.GetDescription()
                         }).ToList();
        }

        [AbpAllowAnonymous]
        public List<DropdownCountOutputDto> GetStatesAndAdsCount(
            long? stateId)
        {
            return _productAdvertManager.GetStatesAndAdsCountQuery(stateId).ToList();
        }

        [AbpAllowAnonymous]
        public List<DropdownCountOutputDto> GetNeighbourhoodAndAdsCount(
            long? cityId,
            long? neighbourhoodId)
        {
            return _productAdvertManager.GetNeighbourhoodAndAdsCountQuery(
                cityId: cityId,
                neighbourhoodId: neighbourhoodId).ToList();
        }

        [AbpAllowAnonymous]
        public List<DropdownCountOutputDto> GetCitiesAndAdsCount(
            long? stateId,
            long? cityId)
        {
            return _productAdvertManager.GetCitiesAndAdsCountQuery(
                stateId: stateId,
                cityId: cityId).ToList();
        }


        private async Task<int> GetProductAdverViews(long id)
        {
            return await _productAdvertViewLogManager
                .GetViewsCountByAdvertId(id);
        }

        private void PopulateAdvertImage(
            List<ProductAdvertViewDto> productAdverts)
        {
            foreach (var productAdvert in productAdverts)
            {
                //TODO : Resolve _fileStorageManager
                productAdvert.PrimaryProductImage = _fileStorageManager
                    .GenerateBlobUrl(
                    productAdvert.PrimaryProductImage,
                    PhoneLeloDataFileType.ProductImages
                    );

                productAdvert.Views = AsyncHelper.RunSync(()=> GetProductAdverViews(productAdvert.Id)) ;
            }
        }

    }
}

