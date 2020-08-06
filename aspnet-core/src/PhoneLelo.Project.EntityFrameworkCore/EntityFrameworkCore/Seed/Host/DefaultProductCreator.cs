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
            
            AddProductCompanyIfNotExists(
                name: "Google",
                displayName: "Google",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "Haier",
                displayName: "Haier",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "Honor",
                displayName: "Honor",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "HTC",
                displayName: "HTC",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "Huawei",
                displayName: "Huawei",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "Infinix",
                displayName: "Infinix",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "Lenovo",
                displayName: "Lenovo",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "LG",
                displayName: "LG",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "Motorola",
                displayName: "Motorola",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "Nokia",
                displayName: "Nokia",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "Oppo",
                displayName: "Oppo",
                productType: ProductTypeEnum.MobilePhone); 
            
            AddProductCompanyIfNotExists(
                name: "OnePlus",
                displayName: "OnePlus",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "Realme",
                displayName: "Realme",
                productType: ProductTypeEnum.MobilePhone); 
            
            AddProductCompanyIfNotExists(
                name: "Sony",
                displayName: "Sony",
                productType: ProductTypeEnum.MobilePhone); 
            
            AddProductCompanyIfNotExists(
                name: "Sony",
                displayName: "Sony",
                productType: ProductTypeEnum.MobilePhone); 
            
            AddProductCompanyIfNotExists(
                name: "vivo",
                displayName: "vivo",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "Xiaomi",
                displayName: "Xiaomi",
                productType: ProductTypeEnum.MobilePhone); 
            
            AddProductCompanyIfNotExists(
                name: "Asus",
                displayName: "Asus",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "BlackBerry",
                displayName: "BlackBerry",
                productType: ProductTypeEnum.MobilePhone);
            
            AddProductCompanyIfNotExists(
                name: "QMobile",
                displayName: "Q-Mobile",
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
