using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using PhoneLelo.Project.Authorization;
using PhoneLelo.Project.Authorization.Accounts;
using PhoneLelo.Project.Authorization.Roles;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.Roles.Dto;
using PhoneLelo.Project.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Uow;
using System;
using System.Reflection;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using PhoneLelo.Project.Import.MobilePhone.Dto;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Product;

namespace PhoneLelo.Project.Import.MobilePhone
{
    public class ProductCompanyAppService : ApplicationService,IProductCompanyAppService
    {
        private readonly IAbpSession _abpSession; 
        private readonly ProductCompanyManager _productCompanyManager;
        private readonly ProductModelManager _productModelManager;

        public ProductCompanyAppService(

            IAbpSession abpSession,
            ProductCompanyManager productCompanyManager,
            ProductModelManager productModelManager)
        {
            _abpSession = abpSession;
            _productCompanyManager = productCompanyManager;
            _productModelManager = productModelManager;
        }

        public async Task<List<DropdownOutputDto>> GetProductCompanyDropdown()
        {
            var productCompanies = await _productCompanyManager.GetAllListAsync();
            var productCompanyDropdown= productCompanies.Select(x => new DropdownOutputDto()
                {
                    Id =x.Id,
                    Name =x.DisplayName
                }).ToList();

            return productCompanyDropdown;
        }
    }
}

