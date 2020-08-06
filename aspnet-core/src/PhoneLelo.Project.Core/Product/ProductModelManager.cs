using Microsoft.AspNetCore.Identity;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using PhoneLelo.Project.Authorization.Roles;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.MultiTenancy;
using Abp.Domain.Services;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Abp.EntityFrameworkCore.Repositories;

namespace PhoneLelo.Project.Authorization
{
    public class ProductModelManager : DomainService
    {
        private readonly IRepository<ProductCompany,long> _productCompanyRepository;
        private readonly IRepository<ProductModel,long> _productModelRepository;

        public ProductModelManager(
            IRepository<ProductCompany, long> productCompanyRepository,
            IRepository<ProductModel, long> productModelRepository)
        {
            _productCompanyRepository = productCompanyRepository;
            _productModelRepository = productModelRepository;
        }

        public async Task<List<ProductModel>> GetAllByCompanyIdAsync(long companyId)
        {
            var productModels = await _productModelRepository
                .GetAll()
                .Where(x => x.ProductCompanyId == companyId)
                .ToListAsync();

            return productModels;
        }
        
        
        public IQueryable<ProductModel> GetAllByCompanyId(long companyId)
        {
            var productModelQuery = _productModelRepository
                .GetAll()
                .Where(x => x.ProductCompanyId == companyId);

            return productModelQuery;
        } 
        
        
        public ProductModel GetByName(
            string modelName,
            long companyId)
        {
            var productModel = _productModelRepository
                .FirstOrDefault(x =>
                x.Model == modelName && 
                x.ProductCompanyId == companyId);

            if (productModel != null)
            {
                return productModel;
            }
            else
            {
                return null;  
            }
        } 
        
        public async Task<ProductModel> GetByNameAsync(
            string modelName,
            long companyId)
        {
            var productModel = await _productModelRepository
                .FirstOrDefaultAsync(x =>
                x.Model == modelName && 
                x.ProductCompanyId == companyId);

            if (productModel != null)
            {
                return productModel;
            }
            else
            {
                return null;  
            }
        }         
        
        public bool IsProductModelExist(
            string modelName,
            long companyId)
        {
            var isProductModelExist = _productModelRepository
                .GetAll()
                .Any(x =>
                x.Model == modelName && 
                x.ProductCompanyId == companyId);

            return isProductModelExist;
        }   
        
        
        public async Task<bool> IsProductModelExistAsync(
            string modelName,
            long companyId)
        {
            var isProductModelExist = await _productModelRepository
                .GetAll()
                .AnyAsync(x =>
                x.Model == modelName && 
                x.ProductCompanyId == companyId);

            return isProductModelExist;
        }  
        
        
        public void InsertPhoneModelList(List<ProductModel> productModels)
        {
             _productModelRepository
                .GetDbContext()
                .AddRange(productModels);

             CurrentUnitOfWork.SaveChanges();
        } 
        
        public async Task InsertPhoneModelListAsync(List<ProductModel> productModels)
        {
            await _productModelRepository
                .GetDbContext()
                .AddRangeAsync(productModels);

            await CurrentUnitOfWork.SaveChangesAsync();
        }   
    }
}
