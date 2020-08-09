using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.EntityFrameworkCore.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneLelo.Project.Authorization
{
    public class ProductAdvertManager : DomainService
    {
        private readonly IRepository<ProductAdvert,long> _productAdvertRepository;
        private readonly IRepository<ProductAdvertBatteryUsage, long> _productAdvertBatteryUsagerepository;
        private readonly IRepository<ProductAdvertImage, long> _productAdvertImageRepository;

        public ProductAdvertManager(
            IRepository<ProductAdvert, long> productAdvertRepository,
            IRepository<ProductAdvertBatteryUsage, long> productAdvertBatteryUsagerepository, IRepository<ProductAdvertImage, long> productAdvertImageRepository)

        {
            _productAdvertRepository = productAdvertRepository;
            _productAdvertBatteryUsagerepository = productAdvertBatteryUsagerepository;
            _productAdvertImageRepository = productAdvertImageRepository;
        }

        public async Task<ProductAdvert> GetByIdAsync(long id)
        {

           var productAdvert =  await _productAdvertRepository
                 .FirstOrDefaultAsync(x=>x.Id==id);

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
        
        public async Task DeleteBatteryUsageAsync(long productAdvertId)
        {
            await _productAdvertBatteryUsagerepository.HardDeleteAsync(x => 
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
    }
}
