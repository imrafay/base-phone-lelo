using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Configuration;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using PhoneLelo.Project.Product.Enum;

namespace PhoneLelo.Project.EntityFrameworkCore.Seed.Host
{
    public class DefaultProductCreator
    {
        private readonly ProjectDbContext _context;

        public DefaultProductCreator(ProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            // Product Company
            AddProductCompanyIfNotExists(
                name: "Apple",
                displayName: "Apple",
                productType: ProductTypeEnum.MobilePhone);  
            
            AddProductCompanyIfNotExists(
                name: "Samsung",
                displayName: "Samsung",
                productType: ProductTypeEnum.MobilePhone);

        }

        private void AddProductCompanyIfNotExists(
            string name,
            string displayName,
            ProductTypeEnum productType)
        {
            if (_context.ProductCompanies
                .IgnoreQueryFilters()
                .Any(s =>s.Name == name &&  s.ProductType == productType))
            {
                return;
            }
            var productCompany = new ProductCompany() 
            {
                Name=name,
                DisplayName=displayName,
                ProductType =productType
            };

            _context.ProductCompanies.Add(productCompany);
            _context.SaveChanges();
        }
    }
}
