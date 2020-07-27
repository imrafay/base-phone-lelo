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
        private readonly int _tenantId = 1;

        public ImportMobilePhoneAppService(

            IAbpSession abpSession)
        {
            _abpSession = abpSession;

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

