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

namespace PhoneLelo.Project.Import.MobilePhone
{
    public class ImportMobilePhoneAppService : ApplicationService, IImportMobilePhoneAppService
    {
        private readonly IAbpSession _abpSession; 
        private readonly ProductCompanyManager _productCompanyManager;
        private readonly ProductModelManager _productModelManager;

        public ImportMobilePhoneAppService(

            IAbpSession abpSession,
            ProductCompanyManager productCompanyManager,
            ProductModelManager productModelManager)
        {
            _abpSession = abpSession;
            _productCompanyManager = productCompanyManager;
            _productModelManager = productModelManager;
        }



        [AbpAllowAnonymous]
        public void CreateOrUpdateProductModels()
        {
            var models = ImportMobilePhones();
            var productModels = new List<ProductModel>();

            foreach (var model in models)
            {
                var productCompanyId = _productCompanyManager.GetIdByName(model.Brand);
                if (productCompanyId==0)
                {
                    continue;
                }

                var isProductModelExist = _productModelManager.IsProductModelExist(
                    modelName: model.Model,
                    companyId: productCompanyId);
                if (isProductModelExist)
                {
                    continue;
                }

                var productModel = MapCsvRowToProductModel(
                    model: model,
                    productCompanyId: productCompanyId);

                if (productModel!=null)
                {
                    productModels.Add(productModel);
                }
            }

            _productModelManager.InsertPhoneModelList(productModels);
        }

        private ProductModel MapCsvRowToProductModel(
            GsmCsvRow model,
            long productCompanyId)
        {
            var productModel = new ProductModel()
            {
                ProductCompanyId =productCompanyId,
                Brand=model.Brand,
                Battery= model.Battery,
                BatteryTalkTime= model.BatteryTalkTime,
                Body= model.Body,
                Display= model.Display,
                LaunchAnnouncedYear= model.LaunchAnnouncedYear,
                MainCameraSingle= model.MainCameraSingle,
                MemoryInternal= model.MemoryInternal,
                NetworkTechnology= model.NetworkTechnology,
                PlatformOS= model.PlatformOS,
                SelfieCameraFeature= model.SelfieCameraFeature,
                Sound= model.Sound,
                DisplaySize= model.DisplaySize,
                DisplayResolution= model.DisplayResolution,
                Features= model.Features,
                FeaturesSensors= model.FeaturesSensors,
                Model= model.Model               
            };
            return productModel;
        }

        [AbpAllowAnonymous]
        public List<GsmCsvRow> ImportMobilePhones()
        {
            var folderPath = "PhoneLelo.Project.Import.MobilePhone";
            var fileContent = GetFileContent($"{folderPath}.gsm.csv");

            using (var reader = new StringReader(fileContent))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;

                var records = csv.GetRecords<GsmCsvRow>().ToList();
                if (!records.Any())
                {
                    return records;

                }
                return records;
            }
        }

        private string GetFileContent(string fileName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(fileName))
                {
                    using (StreamReader reader = new StreamReader(stream))

                        return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
                throw;
            }
        }
    }
}

