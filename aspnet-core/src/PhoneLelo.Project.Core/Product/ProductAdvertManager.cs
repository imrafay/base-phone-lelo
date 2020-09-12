using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Location;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Product.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneLelo.Project.Product
{
    public class ProductAdvertManager : DomainService
    {
        private readonly IRepository<ProductAdvert, long> _productAdvertRepository;
        private readonly IRepository<ProductAdvertBatteryUsage, long> _productAdvertBatteryUsagerepository;
        private readonly IRepository<ProductAdvertImage, long> _productAdvertImageRepository;
        private readonly IRepository<ProductAdvertAccessory, long> _productAdvertAccessoryRepository;
        private readonly IRepository<City, long> _cityRepository;
        private readonly IRepository<State, long> _stateRepository;
        private readonly IRepository<Neighbourhood, long> _neighbourhoodRepository;
        private readonly IRepository<User, long> _userRepository;

        public ProductAdvertManager(
            IRepository<ProductAdvert, long> productAdvertRepository,
            IRepository<ProductAdvertBatteryUsage, long> productAdvertBatteryUsagerepository,
            IRepository<ProductAdvertImage, long> productAdvertImageRepository,
            IRepository<ProductAdvertAccessory, long> productAdvertAccessoryRepository,
            IRepository<City, long> cityRepository,
            IRepository<State, long> stateRepository,
            IRepository<Neighbourhood, long> neighbourhoodRepository,
            IRepository<User, long> userRepository)


        {
            _productAdvertRepository = productAdvertRepository;
            _productAdvertBatteryUsagerepository = productAdvertBatteryUsagerepository;
            _productAdvertImageRepository = productAdvertImageRepository;
            _productAdvertAccessoryRepository = productAdvertAccessoryRepository;
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _neighbourhoodRepository = neighbourhoodRepository;
            _userRepository = userRepository;
        }

        public async Task<ProductAdvert> GetByIdAsync(long id)
        {

            var productAdvert = await _productAdvertRepository
                    .GetAll()
                    .Include(x => x.ProductModelFk)
                    .FirstOrDefaultAsync(x => x.Id == id);

            return productAdvert;
        }

        public async Task CreateAsync(ProductAdvert productAdvert)
        {

            await _productAdvertRepository
                 .InsertAsync(productAdvert);

            await CurrentUnitOfWork
                 .SaveChangesAsync();
        }

        public async Task CreateBatteryUsageAsync(
            List<ProductAdvertBatteryUsage> list)
        {
            await _productAdvertRepository
                 .GetDbContext()
                 .AddRangeAsync(list);

            await CurrentUnitOfWork
                .SaveChangesAsync();
        }

        public async Task CreateProductAdvertImageAsync(
            List<ProductAdvertImage> list)
        {
            await _productAdvertImageRepository
                 .GetDbContext()
                 .AddRangeAsync(list);

            await CurrentUnitOfWork
                .SaveChangesAsync();
        }

        public async Task CreateProductAdvertAccessoryAsync(
            List<ProductAdvertAccessory> list)
        {
            await _productAdvertAccessoryRepository
                 .GetDbContext()
                 .AddRangeAsync(list);

            await CurrentUnitOfWork
                .SaveChangesAsync();
        }


        public async Task UpdateAsync(ProductAdvert productAdvert)
        {

            await _productAdvertRepository
                 .UpdateAsync(productAdvert);

            await CurrentUnitOfWork
                 .SaveChangesAsync();
        }

        public async Task UpdateBatteryUsageAsync(
            List<ProductAdvertBatteryUsage> list,
            long productAdvertId)
        {
            await DeleteBatteryUsageAsync(productAdvertId);
            await CreateBatteryUsageAsync(list);
        }

        public async Task UpdateProductAccessoryAsync(
            List<ProductAdvertAccessory> list,
            long productAdvertId)
        {
            await DeleteProductAccessoryAsync(productAdvertId);
            await CreateProductAdvertAccessoryAsync(list);
        }

        public async Task DeleteBatteryUsageAsync(long productAdvertId)
        {
            await _productAdvertBatteryUsagerepository.HardDeleteAsync(x =>
                x.ProductAdvertId == productAdvertId);

            await CurrentUnitOfWork
                .SaveChangesAsync();
        }

        public async Task DeleteProductAccessoryAsync(long productAdvertId)
        {
            await _productAdvertAccessoryRepository.HardDeleteAsync(x =>
                x.ProductAdvertId == productAdvertId);

            await CurrentUnitOfWork
                .SaveChangesAsync();
        }

        public async Task UpdateProductAdvertImageAsync(
            List<ProductAdvertImage> list)
        {
            foreach (var item in list)
            {
                await _productAdvertImageRepository
                 .UpdateAsync(item);
            }

            await CurrentUnitOfWork
                .SaveChangesAsync();
        }

        public IQueryable<ProductAdvertViewDto> GetAllQuery(
            ProductAdvertFilterInputDto filter)
        {
            var productAdvertQuery = _productAdvertRepository.GetAll()
              .Include(x => x.UserFk)
              .Include(x => x.ProductModelFk)
              .Include(x => x.ProductAdvertAccessories)
              .Include(x => x.ProductAdvertImages)
              .Include(x => x.UserFk)
              .ThenInclude(x => x.StateFk)
              .Include(x => x.UserFk)
              .ThenInclude(x => x.CityFk)
              .Include(x => x.UserFk)
              .ThenInclude(x => x.NeighbourhoodFk)
              .Where(x => x.UserFk != null);

            var output = productAdvertQuery
              .WhereIf(filter.UserId.HasValue, x =>
                     x.UserId == filter.UserId)
              .WhereIf(filter.StateId.HasValue, x =>
                     x.UserFk.StateId == filter.StateId)
              .WhereIf(filter.CityId.HasValue, x =>
                     x.UserFk.CityId == filter.CityId)
              .WhereIf(filter.NeighbourhoodId.HasValue, x =>
                     x.UserFk.NeighbourhoodId == filter.NeighbourhoodId)
              .WhereIf(filter.ProductModelId.HasValue, x =>
                     x.ProductModelId == filter.ProductModelId)
              .WhereIf(filter.ProductCompanyId.HasValue, x =>
                     x.ProductModelFk.ProductCompanyId == filter.ProductCompanyId)
              .WhereIf(string.IsNullOrEmpty(filter.NameFilter) == false, x =>
                     x.ProductModelFk.Model.ToLower().Contains(filter.NameFilter) ||
                     x.ProductModelFk.Brand.ToLower().Contains(filter.NameFilter))
              .WhereIf(filter.RamFilter.Any(), x =>
                     filter.RamFilter.Contains(x.Ram))
              .WhereIf(filter.StorageFilter.Any(), x =>
                     filter.StorageFilter.Contains(x.Storage))
              .WhereIf(filter.IsNew.HasValue, x =>
                    x.IsNew == filter.IsNew)
              .WhereIf(filter.IsPtaApproved.HasValue, x =>
                    x.IsPtaApproved == filter.IsPtaApproved)
              .WhereIf(filter.IsSpot.HasValue, x =>
                    x.IsSpot == filter.IsSpot)
              .WhereIf(filter.IsDamage.HasValue, x =>
                    x.IsDamage == filter.IsDamage)
              .WhereIf(filter.IsNegotiable.HasValue, x =>
                    x.IsNegotiable == filter.IsNegotiable)
              .WhereIf(filter.IsExchangeable.HasValue, x =>
                    x.IsExchangeable == filter.IsExchangeable)
              .Select(x => new ProductAdvertViewDto()
              {
                  Id = x.Id,
                  Views = 0,
                  Ram = x.Ram,
                  Price = x.Price,
                  IsNew = x.IsNew,
                  Storage = x.Storage,
                  IsPtaApproved = x.IsPtaApproved,
                  AdvertPostedDate = x.CreationTime,
                  ProductModelId = x.ProductModelId,
                  ProductModelName = x.ProductModelFk.Model,
                  ProductCompanyName = x.ProductModelFk.Brand,
                  PrimaryProductImage = x.ProductAdvertImages
                                .Where(i => i.ImagePriority == ProductImagePriorityEnum.Primary)
                                .Select(n => n.Image)
                                .FirstOrDefault(),
                  UserFullName = x.UserFk.FullName,
                  State = x.UserFk.StateFk != null ? x.UserFk.StateFk.Name : string.Empty,
                  City = x.UserFk.CityFk != null ? x.UserFk.CityFk.Name : string.Empty,
                  Neighbourhood = x.UserFk.NeighbourhoodFk != null ? x.UserFk.NeighbourhoodFk.Name : string.Empty
              });

            return output;

        }


        public async Task<List<ProductAdvertImage>> GetProducAdverImagesById(
            long productAdvertId)
        {

            var productAdvertImages = await _productAdvertImageRepository
                  .GetAll()
                  .Where(x => x.ProductAdvertId == productAdvertId)
                  .ToListAsync();

            return productAdvertImages;
        }

        public async Task<List<ProductAdvertBatteryUsage>> GetProducAdverBatteryUsagesById(
            long productAdvertId)
        {

            var productAdvertBatteryUsages = await _productAdvertBatteryUsagerepository
                  .GetAll()
                  .Where(x => x.ProductAdvertId == productAdvertId)
                  .ToListAsync();

            return productAdvertBatteryUsages;
        }

        public async Task<List<ProductAdvertAccessory>> GetProductAdvertAccessoriesById(
            long productAdvertId)
        {

            var productAdvertAccessories = await _productAdvertAccessoryRepository
                  .GetAll()
                  .Where(x => x.ProductAdvertId == productAdvertId)
                  .ToListAsync();

            return productAdvertAccessories;
        }

        public IQueryable<DropdownCountOutputDto> GetStatesAndAdsCountQuery(
            long? stateId)
        {
            var output = from state in _stateRepository.GetAll()
                          .WhereIf(stateId.HasValue, x => x.Id == stateId)

                         join user in _userRepository.GetAll()
                         on state.Id equals user.StateId

                         join ad in _productAdvertRepository.GetAll()
                         on user.Id equals ad.UserId

                         group ad by new { state.Id, state.Name } into stateAds

                         select new DropdownCountOutputDto
                         {
                             Id = stateAds.Key.Id,
                             Name = stateAds.Key.Name,
                             Count = stateAds.Count()
                         };

            return output;
        }
        public IQueryable<DropdownCountOutputDto> GetNeighbourhoodAndAdsCountQuery(
            long? cityId,
            long? neighbourhoodId)
        {
            var output = from neighbourhood in _neighbourhoodRepository.GetAll()
                          .WhereIf(cityId.HasValue, x => x.CityId == cityId)
                          .WhereIf(neighbourhoodId.HasValue, x => x.Id == neighbourhoodId)

                         join user in _userRepository.GetAll()
                         on neighbourhood.Id equals user.NeighbourhoodId

                         join ad in _productAdvertRepository.GetAll()
                         on user.Id equals ad.UserId

                         group ad by new { neighbourhood.Id, neighbourhood.Name } into neighbourhoodAds

                         select new DropdownCountOutputDto
                         {
                             Id = neighbourhoodAds.Key.Id,
                             Name = neighbourhoodAds.Key.Name,
                             Count = neighbourhoodAds.Count()
                         };

            return output;
        }

        public IQueryable<DropdownCountOutputDto> GetCitiesAndAdsCountQuery(
            long? stateId,
            long? cityId
            )
        {
            var output = from city in _cityRepository.GetAll()
                           .WhereIf(stateId.HasValue, x => x.StateId == stateId)
                           .WhereIf(cityId.HasValue, x => x.Id == cityId)

                         join user in _userRepository.GetAll()
                         on city.Id equals user.CityId

                         join ad in _productAdvertRepository.GetAll()
                         on user.Id equals ad.UserId

                         group ad by new { city.Id, city.Name } into cityAds

                         select new DropdownCountOutputDto
                         {
                             Id = cityAds.Key.Id,
                             Name = cityAds.Key.Name,
                             Count = cityAds.Count()
                         };

            return output;
        }

    }
}
