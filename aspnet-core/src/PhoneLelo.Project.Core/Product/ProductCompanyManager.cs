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
using System.Collections.Generic;
using System.Linq;

namespace PhoneLelo.Project.Product
{
    public class ProductCompanyManager : DomainService
    {
        private readonly IRepository<ProductCompany, long> _productCompanyRepository;

        public ProductCompanyManager(
            IRepository<ProductCompany, long> productCompanyRepository)
        {
            _productCompanyRepository = productCompanyRepository;
        }


        public IQueryable<ProductCompany> GetAll()
        {
            var productCompanyQuery = _productCompanyRepository
                .GetAll();

            return productCompanyQuery;
        }

        public async Task<List<ProductCompany>> GetAllListAsync()
        {
            var productCompanies = await _productCompanyRepository
                .GetAllListAsync();

            return productCompanies;
        }

        public ProductCompany GetByName(string companyName)
        {
            var productCompany = _productCompanyRepository
                .FirstOrDefault(x => x.Name == companyName);

            if (productCompany != null)
            {
                return productCompany;
            }
            else
            {
                return null;
            }
        }

        public long GetIdByName(string companyName)
        {
            var productCompany = _productCompanyRepository
                .FirstOrDefault(x => x.Name == companyName);

            if (productCompany != null)
            {
                return productCompany.Id;
            }
            else
            {
                return 0;
            }
        }

        public async Task<ProductCompany> GetByNameAsync(string companyName)
        {
            var productCompany = await _productCompanyRepository
                    .FirstOrDefaultAsync(x => x.Name == companyName);

            if (productCompany != null)
            {
                return productCompany;
            }
            else
            {
                return null;
            }
        }

        public async Task<long> GetIdByNameAsync(string companyName)
        {
            var productCompany = await _productCompanyRepository
                    .FirstOrDefaultAsync(x => x.Name == companyName);

            if (productCompany != null)
            {
                return productCompany.Id;
            }
            else
            {
                return 0;
            }
        }
    }
}
